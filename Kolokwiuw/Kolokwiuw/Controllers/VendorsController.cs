using Kolokwiuw.DTOs;
using Kolokwiuw.Exceptions;
using Kolokwiuw.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorsController : ControllerBase
{
    private readonly IDbService _dbService;

    public VendorsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetDetails(string code)
    {
        try
        {
            var result = await _dbService.GetVendorAsync(code);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateVendorDTO dto)
    {
        try
        {
            await _dbService.AddAsync(dto);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
    }
}