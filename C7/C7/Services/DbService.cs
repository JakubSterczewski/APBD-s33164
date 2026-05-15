using C7.Data;
using C7.DTOs;
using C7.Entities;
using C7.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace C7.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _dbContext;

    public DbService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetPcsDto>> GetAllPcsAsync()
    {
        var res = await _dbContext.PCs.Select(pc => new GetPcsDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        }).ToListAsync();
        
        if (res == null) throw new NotFoundException();

        return res;
    }
    
    public async Task<GetPcDetailsDto> GetPcsDetailsByIdAsync(int id)
    {
        var res = await _dbContext.PCs
            .Where(pc => pc.Id  == id)
            .Select(pc => new GetPcDetailsDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock,
                Components = pc.PcComponents.Select(pcc => new GetComponentDetailsDto
                {
                    Amount = pcc.Amount,
                    Component = new GetComponentDto
                    {
                        Code = pcc.Component.Code,
                        Name = pcc.Component.Name,
                        Description = pcc.Component.Descritpion,
                        Manufacturer = new GetManufacturerDto
                        {
                            Id = pcc.Component.ComponentManufacturer.Id,
                            Abbreviation = pcc.Component.ComponentManufacturer.Abbreviation,
                            FullName = pcc.Component.ComponentManufacturer.FullName,
                            FouncationDate = pcc.Component.ComponentManufacturer.FoundationDate
                        },
                        Type = new GetTypeDto
                        {
                            Id = pcc.Component.ComponentType.Id,
                            Abbreviations = pcc.Component.ComponentType.Abbreviation,
                            Name = pcc.Component.ComponentType.Name
                        }
                    }
                }).ToList()
            }).FirstOrDefaultAsync();

        if (res == null) throw new NotFoundException();

        return res;
    }
    
    public async Task<GetPcDto> AddPcAsync(AddPcRequestDto addPcDto)
    {
        var pc = new Pc()
        {
            Name = addPcDto.Name,
            Weight = addPcDto.Weight,
            Warranty = addPcDto.Warranty,
            CreatedAt = addPcDto.CreatedAt,
            Stock = addPcDto.Stock
        };
        await _dbContext.AddAsync(pc);
        await _dbContext.SaveChangesAsync();

        return new GetPcDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }
    
    public async Task<GetPcDto> GetPcByIdAsync(int id)
    {
        var res = await _dbContext.PCs
            .Where(pc => pc.Id == id)
            .Select(pc => new GetPcDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            }).FirstOrDefaultAsync();

        if (res == null) throw new NotFoundException();
        
        return res;
    }

    public async Task UpdatePcByIdAsync(int id, PutPcDto putPcDto)
    {
        var pc = await _dbContext.PCs.FirstOrDefaultAsync(pc => pc.Id == id);
        if (pc == null) throw new NotFoundException();

        pc.Name = putPcDto.Name;
        pc.Weight = putPcDto.Weight;
        pc.Warranty = putPcDto.Warranty;
        pc.CreatedAt = putPcDto.CreatedAt;
        pc.Stock = putPcDto.Stock;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePcByIdAsync(int id)
    {
        var pc = await _dbContext.PCs.FirstOrDefaultAsync(pc => pc.Id == id);
        if (pc == null) throw new NotFoundException();

        _dbContext.PCs.Remove(pc);
        await _dbContext.SaveChangesAsync();
    }
}