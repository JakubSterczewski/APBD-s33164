using Microsoft.AspNetCore.Mvc;
using System_Uznawania_Przychodów.DTOs.Clients;
using System_Uznawania_Przychodów.Exceptions;
using System_Uznawania_Przychodów.Services;

namespace System_Uznawania_Przychodów.Controllers;

[Route("api/clients")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("individual")]
    public async Task<IActionResult> AddIndividualClient([FromBody] AddIndividualClientDto dto)
    {
        try
        {
            await _clientService.AddIndividualClientAsync(dto);
            return Created();
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPost("company")]
    public async Task<IActionResult> AddCompanyClient([FromBody] AddCompanyClientDto dto)
    {
        try
        {
            await _clientService.AddCompanyClientAsync(dto);
            return Created();
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut("individual/{id}")]
    public async Task<IActionResult> UpdateIndividualClient(int id, [FromBody] UpdateIndividualClientDto dto)
    {
        try
        {
            await _clientService.UpdateIndividualClientAsync(id, dto);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut("company/{id}")]
    public async Task<IActionResult> UpdateCompanyClient(int id, [FromBody] UpdateCompanyClientDto dto)
    {
        try
        {
            await _clientService.UpdateCompanyClientAsync(id, dto);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("individual/{id}")]
    public async Task<IActionResult> DeleteIndividualClient(int id)
    {
        try
        {
            await _clientService.DeleteIndividualClientAsync(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
