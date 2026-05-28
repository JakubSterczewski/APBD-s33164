using Kolokwium2.DTOs;
using Kolokwium2.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PatientsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatientsAsync(string? lastName)
    {
        var res = await _dbService.GetPatientsAsync(lastName);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPatientAsync(AddPatientDto addPatientDto)
    {
        try
        {
            await _dbService.AddPatientAsync(addPatientDto);
            return Created();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);  // nie znalazlo doc
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);  // zla data
        }
    }
}