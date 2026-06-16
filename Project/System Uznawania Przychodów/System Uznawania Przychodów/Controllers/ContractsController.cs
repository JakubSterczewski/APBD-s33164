using Microsoft.AspNetCore.Mvc;
using System_Uznawania_Przychodów.DTOs.Contracts;
using System_Uznawania_Przychodów.Exceptions;
using System_Uznawania_Przychodów.Services;

namespace System_Uznawania_Przychodów.Controllers;

[Route("api/contracts")]
[ApiController]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] AddContractDto dto)
    {
        try
        {
            var id = await _contractService.CreateContractAsync(dto);
            return Created();
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
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

    [HttpPost("{id}/payments")]
    public async Task<IActionResult> AddPayment(int id, [FromBody] AddContractPaymentDto dto)
    {
        try
        {
            await _contractService.AddPaymentAsync(id, dto);
            return Created();
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
}
