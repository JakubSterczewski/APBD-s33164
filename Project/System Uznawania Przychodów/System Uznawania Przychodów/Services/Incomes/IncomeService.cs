using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System_Uznawania_Przychodów.Data;
using System_Uznawania_Przychodów.DTOs.Income;
using System_Uznawania_Przychodów.Exceptions;

namespace System_Uznawania_Przychodów.Services;

public class IncomeService : IIncomeService
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;

    public IncomeService(AppDbContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    public async Task<GetIncomeDto> CalculateActualIncome(string? currency)
    {
        var contractIncome = await _context.Contracts
            .Where(c => c.SignedAt != null)
            .SumAsync(c => c.Price);

        var subscriptionIncome = await _context.SubscriptionPayments
            .SumAsync(p => p.Amount);

        var total = contractIncome + subscriptionIncome;
        return await ToDto(total, currency);
    }

    public async Task<GetIncomeDto> CalculateActualIncomeForSoftwareId(int id, string? currency)
    {
        var softwareExists = await _context.Softwares.AnyAsync(s => s.Id == id);
        if (!softwareExists)
        {
            throw new NotFoundException($"Software with id {id} does not exist");
        }

        var contractIncome = await _context.Contracts
            .Where(c => c.SoftwareId == id && c.SignedAt != null)
            .SumAsync(c => c.Price);

        var subscriptionIncome = await _context.SubscriptionPayments
            .Where(p => p.Subscription.SoftwareId == id)
            .SumAsync(p => p.Amount);

        var total = contractIncome + subscriptionIncome;
        return await ToDto(total, currency);
    }

    public async Task<GetIncomeDto> CalculateEstimatedIncome(string? currency)
    {
        var contractsIncome = await _context.Contracts
            .Where(c => c.SignedAt != null || (c.SignedAt == null && c.IsActive))
            .SumAsync(c => c.Price);

        var subscriptionIncome = await _context.SubscriptionPayments
            .SumAsync(p => p.Amount);

        var subscriptionNextRenewal = await _context.Subscriptions
            .Where(s => !s.IsCancelled)
            .SumAsync(s => s.RenewalPrice);

        var total = contractsIncome + subscriptionIncome + subscriptionNextRenewal;
        return await ToDto(total, currency);
    }

    public async Task<GetIncomeDto> CalculateEstimatedIncomeForSoftwareId(int id, string? currency)
    {
        var softwareExists = await _context.Softwares.AnyAsync(s => s.Id == id);
        if (!softwareExists)
        {
            throw new NotFoundException($"Software with id {id} does not exist");
        }
        
        var contractsIncome = await _context.Contracts
            .Where(c => c.SoftwareId == id && (c.SignedAt != null || (c.SignedAt == null && c.IsActive)))
            .SumAsync(c => c.Price);

        var subscriptionIncome = await _context.SubscriptionPayments
            .Where(p => p.Subscription.SoftwareId == id)
            .SumAsync(p =>  p.Amount);

        var subscriptionNextRenewal = await _context.Subscriptions
            .Where(s => s.SoftwareId == id && !s.IsCancelled)
            .SumAsync(s => s.RenewalPrice);
        
        var total = contractsIncome + subscriptionIncome + subscriptionNextRenewal;
        return await ToDto(total, currency);
    }

    private async Task<GetIncomeDto> ToDto(decimal amountPln, string? currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Equals("PLN", StringComparison.OrdinalIgnoreCase))
        {
            return new GetIncomeDto { Amount = amountPln, Currency = "PLN" };
        }
        
        var rate = await GetNbpRateAsync(currency.ToUpper());
        return new GetIncomeDto
        {
            Amount = Math.Round(amountPln / rate, 2),
            Currency = currency.ToUpper()
        };
    }

    private async Task<decimal> GetNbpRateAsync(string currencyCode)
    {
        var response = await _httpClient.GetAsync($"https://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/?format=json");

        if (!response.IsSuccessStatusCode)
        {
            response = await _httpClient.GetAsync($"https://api.nbp.pl/api/exchangerates/rates/b/{currencyCode}/?format=json");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundException($"This currency {currencyCode} is not supported by NBP.");
            }
        }

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var mid = doc.RootElement
            .GetProperty("rates")[0]
            .GetProperty("mid")
            .GetDecimal();

        return mid;
    }
}
