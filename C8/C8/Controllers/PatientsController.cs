using C8.DTOs;
using C8.Exceptions;
using C8.Services;
using Microsoft.AspNetCore.Mvc;

namespace C8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PatientsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients(string? search)
    {
        var res = await _dbService.GetPatientsAsync(search);
        return Ok(res);
    }

    [HttpPost("{pesel}/bedassignments")]
    public async Task<IActionResult> AddBedAssignment(string pesel, addBedAssignmentDto addBedAssignmentDto)
    {
        try
        {
            await _dbService.AddBedAssignmentAsync(pesel, addBedAssignmentDto);
            ;
            return Created();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}