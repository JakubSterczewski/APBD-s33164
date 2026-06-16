using Microsoft.AspNetCore.Mvc;
using System_Uznawania_Przychodów.Exceptions;
using System_Uznawania_Przychodów.Services;

namespace System_Uznawania_Przychodów.Controllers;

[Route("api/{controller}")]
[ApiController]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> CalculateActualIncome(string? currency)
    {
        var res = await _incomeService.CalculateActualIncome(currency);
        return Ok(res);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> CalculateActualIncomeForSoftwareId(string id, string? currency)
    {
        try
        {
            var res = await _incomeService.CalculateActualIncomeForSoftwareId(int.Parse(id), currency);
            return Ok(res);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("ectimated")]
    public async Task<IActionResult> CalculateEstimatedIncome(string? productName)
    {
        var res = await _incomeService.CalculateEstimatedIncome(productName);
        return Ok(res);
    }
    
    [HttpGet("ectimated/{id}")]
    public async Task<IActionResult> CalculateEstimatedIncomeForSoftwareId(string id, string? currency)
    {
        try
        {
            var res = await _incomeService.CalculateEstimatedIncomeForSoftwareId(int.Parse(id), currency);
            return Ok(res);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}