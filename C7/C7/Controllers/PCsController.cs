using C7.DTOs;
using C7.Exceptions;
using C7.Services;
using Microsoft.AspNetCore.Mvc;

namespace C7.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PCsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PCsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPcs()
    {
        try
        {
            var res = await _dbService.GetAllPcsAsync();;
            return Ok(res);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetPcsDetailsById(string id)
    {
        try
        {
            var res = await _dbService.GetPcsDetailsByIdAsync(int.Parse(id));
            return Ok(res);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPc(AddPcRequestDto addPcRequestDto)
    {
        var res = await _dbService.AddPcAsync(addPcRequestDto);
        return CreatedAtAction(nameof(GetPcById), new { id = res.Id }, res);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPcById(string id)
    {
        try
        {
            var res = await _dbService.GetPcByIdAsync(int.Parse(id));
            return Ok(res);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePcById(string id, PutPcDto putPcDto)
    {
        try
        {
            await _dbService.UpdatePcByIdAsync(int.Parse(id), putPcDto);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePcById(string id)
    {
        try
        {
            await _dbService.DeletePcByIdAsync(int.Parse(id));
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}