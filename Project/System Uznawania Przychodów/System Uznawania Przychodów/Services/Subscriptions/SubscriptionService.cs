using Microsoft.EntityFrameworkCore;
using System_Uznawania_Przychodów.Data;
using System_Uznawania_Przychodów.Domain.Entities;
using System_Uznawania_Przychodów.Domain.Enums;
using System_Uznawania_Przychodów.DTOs.Subscriptions;
using System_Uznawania_Przychodów.Exceptions;

namespace System_Uznawania_Przychodów.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly AppDbContext _context;

    public SubscriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddSubscription(AddSubscriptionDto dto)
    {
        if (dto.RenewalPeriodMonths < 1 || dto.RenewalPeriodMonths > 24)
        {
            throw new BadRequestException("Subscription duration must be between 1 and 24 months.");
        }
        
        var clientExists = await _context.Clients.AnyAsync(c => c.Id == dto.ClientId);
        if (!clientExists)
        {
            throw new NotFoundException($"Client with id {dto.ClientId} does not exist.");
        }

        var software = await _context.Softwares
            .Include(s => s.SoftwareDiscounts)
                .ThenInclude(sd => sd.Discount)
            .FirstOrDefaultAsync(s => s.Id == dto.SoftwareId);
        if (software is null)
        {
            throw new NotFoundException($"Software with id {dto.SoftwareId} does not exist.");
        }
        
        var hasActiveSubscription = await _context.Subscriptions.AnyAsync(s =>
            s.ClientId == dto.ClientId &&
            s.SoftwareId == dto.SoftwareId &&
            !s.IsCancelled);
        if (hasActiveSubscription)
        {
            throw new ConflictException("Client already has an active subscription for this software.");
        }
        
        var now = DateTime.Now;
        var basePrice = software.MonthlySubscriptionPrice * dto.RenewalPeriodMonths;

        var softwareDiscountMax = software.SoftwareDiscounts
            .Select(sd => sd.Discount)
            .Where(d => d.PurchaseModel == PurchaseModel.Subscription && d.StartDate <= now && d.EndDate >= now)
            .Select(d => d.Percentage)
            .DefaultIfEmpty(0)
            .Max();

        var globalDiscountMax = await _context.Discounts
            .Where(d => d.IsOnAllProducts && d.PurchaseModel == PurchaseModel.Subscription && d.StartDate <= now && d.EndDate >= now)
            .Select(d => d.Percentage)
            .DefaultIfEmpty(0)
            .MaxAsync();
        
        var bestDiscount = Math.Max(softwareDiscountMax, globalDiscountMax);

        var isReturning = await _context.Contracts.AnyAsync(c => c.ClientId == dto.ClientId && c.SignedAt != null)
                          || await _context.Subscriptions.AnyAsync(s => s.ClientId == dto.ClientId);
        var loyaltyDiscount = isReturning ? 5 : 0;

        var renewalPrice = Math.Round(basePrice * (1 - loyaltyDiscount / 100), 2);

        var firstPayment = Math.Round(basePrice * (1 - (bestDiscount + loyaltyDiscount) / 100m), 2);

        var subscription = new Subscription
        {
            ClientId = dto.ClientId,
            SoftwareId = dto.SoftwareId,
            Name = dto.Name,
            RenewalPeriodMonths = dto.RenewalPeriodMonths,
            RenewalPrice = renewalPrice,
            StartDate = now,
            NextPeriodStart = now.AddMonths(dto.RenewalPeriodMonths),
            HasPaid = true,
            IsCancelled = false
        };

        _context.Subscriptions.Add(subscription);

        _context.SubscriptionPayments.Add(new SubscriptionPayment
        {
            Subscription = subscription,
            Amount = firstPayment,
            PaidAt = now
        });

        await _context.SaveChangesAsync();
        return subscription.Id;
    }

    public async Task AddPaymentForSubscription(int id, AddPaymentForSubscriptionDto dto)
    {
        var subscription = await _context.Subscriptions.FindAsync(id);
        if (subscription is null)
        {
            throw new NotFoundException($"Subscription with id {id} does not exist.");
        }
        
        if (subscription.IsCancelled)
        {
            throw new ConflictException("This subscription has been cancelled.");
        }
        
        var now = DateTime.Now;
        var periodEnd = subscription.NextPeriodStart.AddMonths(subscription.RenewalPeriodMonths);

        if (now < subscription.NextPeriodStart)
        {
            throw new ConflictException("Current payment period has been already paid.");
        }
        
        if (now >= periodEnd)
        { 
            subscription.IsCancelled = true;
            await _context.SaveChangesAsync();
            throw new ConflictException("Deadline of payment expired");
        }
        
        var expectedAmount = Math.Round(subscription.RenewalPrice, 2);
        if (dto.Amount != expectedAmount)
            throw new BadRequestException($"Amount has to be equal {expectedAmount:F2} PLN.");

        subscription.NextPeriodStart = subscription.NextPeriodStart.AddMonths(subscription.RenewalPeriodMonths);
        subscription.HasPaid = true;

        _context.SubscriptionPayments.Add(new SubscriptionPayment
        {
            SubscriptionId = subscription.Id,
            Amount = dto.Amount,
            PaidAt = now
        });

        await _context.SaveChangesAsync();
    }
}
