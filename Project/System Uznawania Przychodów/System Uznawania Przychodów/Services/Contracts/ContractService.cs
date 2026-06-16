using Microsoft.EntityFrameworkCore;
using System_Uznawania_Przychodów.Data;
using System_Uznawania_Przychodów.Domain.Entities;
using System_Uznawania_Przychodów.Domain.Enums;
using System_Uznawania_Przychodów.DTOs.Contracts;
using System_Uznawania_Przychodów.Exceptions;

namespace System_Uznawania_Przychodów.Services;

public class ContractService : IContractService
{
    private readonly AppDbContext _context;

    public ContractService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateContractAsync(AddContractDto dto)
    {
        var duration = dto.EndDate.DayNumber - dto.StartDate.DayNumber;
        if (duration < 3 || duration > 30)
        {
            throw new BadRequestException("Contract duration must be between 3 and 30 days.");
        }
        
        if (dto.AdditionalSupportYears < 0 || dto.AdditionalSupportYears > 3)
        {
            throw new BadRequestException("Additional support years must be 0, 1, 2 or 3.");
        }
        
        var client = await _context.Clients.FindAsync(dto.ClientId);
        if (client is null || client.DeletedAt != null)
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
        
        var versionExists = await _context.SoftwareVersions
            .AnyAsync(v => v.SoftwareId == dto.SoftwareId && v.Version == dto.SoftwareVersion);
        if (!versionExists)
        {
            throw new NotFoundException($"Version {dto.SoftwareVersion} does not exist for this software.");
        }
        
        var hasActiveContract = await _context.Contracts.AnyAsync(c =>
            c.ClientId == dto.ClientId &&
            c.SoftwareId == dto.SoftwareId &&
            c.IsActive &&
            c.SignedAt == null);
        if (hasActiveContract)
        {
            throw new ConflictException("Client already has an active contract for this software.");
        }
        
        var hasActiveSubscription = await _context.Subscriptions.AnyAsync(s =>
            s.ClientId == dto.ClientId &&
            s.SoftwareId == dto.SoftwareId &&
            !s.IsCancelled);
        if (hasActiveSubscription)
        {
            throw new ConflictException("Client already has an active subscription for this software.");
        }
        
        var basePrice = software.YearlyLicensePrice + dto.AdditionalSupportYears * 1000;
        var now = DateTime.Now;

        var softwareDiscountMax = software.SoftwareDiscounts
            .Select(sd => sd.Discount)
            .Where(d => d.PurchaseModel == PurchaseModel.License && d.StartDate <= now && d.EndDate >= now)
            .Select(d => d.Percentage)
            .DefaultIfEmpty(0)
            .Max();

        var globalDiscountMax = await _context.Discounts
            .Where(d => d.IsOnAllProducts && d.PurchaseModel == PurchaseModel.License && d.StartDate <= now && d.EndDate >= now)
            .Select(d => d.Percentage)
            .DefaultIfEmpty(0)
            .MaxAsync();

        var bestDiscount = Math.Max(softwareDiscountMax, globalDiscountMax);

        var isReturning = await _context.Contracts.AnyAsync(c => c.ClientId == dto.ClientId && c.SignedAt != null) 
                          || await _context.Subscriptions.AnyAsync(s => s.ClientId == dto.ClientId);
        var returningDiscount = isReturning ? 5 : 0;

        var price = Math.Round(basePrice * (1 - (bestDiscount + returningDiscount) / 100m), 2);

        var contract = new Contract
        {
            ClientId = dto.ClientId,
            SoftwareId = dto.SoftwareId,
            SoftwareVersion = dto.SoftwareVersion,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Price = price,
            AdditionalSupportYears = dto.AdditionalSupportYears,
            IsActive = true
        };

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();
        return contract.Id;
    }

    public async Task AddPaymentAsync(int contractId, AddContractPaymentDto dto)
    {
        var contract = await _context.Contracts.FindAsync(contractId);
        if (contract is null)
        {
            throw new NotFoundException($"Contract with id {contractId} does not exist.");
        }
        
        if (contract.SignedAt != null)
        {
            throw new ConflictException("This contract has been already paid.");
        }
        
        var nowDate = DateOnly.FromDateTime(DateTime.UtcNow);

        if (!contract.IsActive || nowDate > contract.EndDate)
        {
            if (contract.IsActive)
            {
                contract.IsActive = false;
                await _context.SaveChangesAsync();
            }
            throw new ConflictException("Deadline of the contract has expired.");
        }
        
        if (nowDate < contract.StartDate)
        {
            throw new ConflictException("Payment before start date of the contract.");
        }
        
        var remaining = contract.Price - contract.TotalPaid;
        if (dto.Amount <= 0)
            throw new BadRequestException("Amount must be greater than zero.");
        if (dto.Amount > remaining)
            throw new BadRequestException($"Amount exceeds remaining amount ({remaining:F2} PLN).");

        contract.TotalPaid += dto.Amount;

        _context.ContractPayments.Add(new ContractPayment
        {
            ContractId = contractId,
            Amount = dto.Amount,
            PaidAt = DateTime.Now
        });

        if (contract.TotalPaid == contract.Price)
            contract.SignedAt = nowDate;

        await _context.SaveChangesAsync();
    }
}
