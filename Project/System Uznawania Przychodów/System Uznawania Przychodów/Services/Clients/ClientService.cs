using Microsoft.EntityFrameworkCore;
using System_Uznawania_Przychodów.Data;
using System_Uznawania_Przychodów.Domain.Entities;
using System_Uznawania_Przychodów.DTOs.Clients;
using System_Uznawania_Przychodów.Exceptions;

namespace System_Uznawania_Przychodów.Services;

public class ClientService : IClientService
{
    private readonly AppDbContext _context;

    public ClientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddIndividualClientAsync(AddIndividualClientDto dto)
    {
        var peselExists = await _context.IndividualClients.AnyAsync(c => c.Pesel == dto.Pesel);
        if (peselExists)
        {
            throw new ConflictException($"Client with this pesel {dto.Pesel} already exists.");
        }

        var client = new Client
        {
            Address = dto.Address,
            Email = dto.Email,
            Phone = dto.Phone
        };

        var individual = new IndividualClient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Pesel = dto.Pesel,
            Client = client
        };

        _context.IndividualClients.Add(individual);
        await _context.SaveChangesAsync();
        return individual.ClientId;
    }

    public async Task<int> AddCompanyClientAsync(AddCompanyClientDto dto)
    {
        var krsExists = await _context.CompanyClients.AnyAsync(c => c.Krs == dto.Krs);
        if (krsExists)
        {
            throw new ConflictException($"Company with this krs {dto.Krs} already exists.");
        }

        var client = new Client
        {
            Address = dto.Address,
            Email = dto.Email,
            Phone = dto.Phone
        };

        var company = new CompanyClient
        {
            CompanyName = dto.CompanyName,
            Krs = dto.Krs,
            Client = client
        };

        _context.CompanyClients.Add(company);
        await _context.SaveChangesAsync();
        return company.ClientId;
    }

    public async Task UpdateIndividualClientAsync(int id, UpdateIndividualClientDto dto)
    {
        var individual = await _context.IndividualClients
            .Include(c => c.Client)
            .FirstOrDefaultAsync(c => c.ClientId == id);

        if (individual is null)
        {
            throw new NotFoundException($"Client with id {id} does not exist.");
        }
        
        if (individual.Client.DeletedAt != null)
        {
            throw new ConflictException($"Client with id {id} has beem already deleted.");
        }
        
        individual.FirstName = dto.FirstName;
        individual.LastName = dto.LastName;
        individual.Client.Address = dto.Address;
        individual.Client.Email = dto.Email;
        individual.Client.Phone = dto.Phone;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateCompanyClientAsync(int id, UpdateCompanyClientDto dto)
    {
        var company = await _context.CompanyClients
            .Include(c => c.Client)
            .FirstOrDefaultAsync(c => c.ClientId == id);

        if (company is null)
        {
            throw new NotFoundException($"Company with id {id} does not exist.");
        }
        
        company.CompanyName = dto.CompanyName;
        company.Client.Address = dto.Address;
        company.Client.Email = dto.Email;
        company.Client.Phone = dto.Phone;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteIndividualClientAsync(int id)
    {
        var individual = await _context.IndividualClients
            .Include(c => c.Client)
            .FirstOrDefaultAsync(c => c.ClientId == id);

        if (individual is null || individual.Client.DeletedAt != null)
        {
            throw new NotFoundException($"Client with id {id} does not exist.");
        }

        if (individual.Client.DeletedAt != null)
        {
            throw new ConflictException($"Client with id {id} has beem already deleted.");
        }
        
        individual.FirstName = "";
        individual.LastName = "";
        individual.Client.Address = "";
        individual.Client.Email = "";
        individual.Client.Phone = "";
        individual.Client.DeletedAt = DateTime.Now;

        await _context.SaveChangesAsync();
    }
}
