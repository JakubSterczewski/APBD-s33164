using Kolokwiuw.DTOs;

namespace Kolokwiuw.Services;

public interface IDbService
{
    Task<GetVendorDetailsDTO> GetVendorAsync(string code);
    Task AddAsync(CreateVendorDTO dto);
}