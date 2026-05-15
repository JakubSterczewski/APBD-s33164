using C7.DTOs;

namespace C7.Services;

public interface IDbService
{
    Task<IEnumerable<GetPcsDto>> GetAllPcsAsync();
    Task<GetPcDetailsDto> GetPcsDetailsByIdAsync(int id);
    Task<GetPcDto> AddPcAsync(AddPcRequestDto addPcDto);
    Task<GetPcDto> GetPcByIdAsync(int id);
    Task UpdatePcByIdAsync(int id, PutPcDto putPcDto);
    Task DeletePcByIdAsync(int id);
}