using C5.Models;
using C5.Services;
using Microsoft.AspNetCore.Mvc;

namespace C5;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public IActionResult GetRooms(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {
        if (minCapacity == null && hasProjector == null && activeOnly == null)
            return Ok(_roomService.GetAll());

        var rooms = _roomService.GetRoom(minCapacity ?? 0, hasProjector ?? false, activeOnly ?? false);

        if (!rooms.Any())
            return NotFound("No rooms with these filters");

        return Ok(rooms);
    }

    [HttpGet("{idRoom}")]
    public IActionResult GetRoom(int idRoom)
    {
        var room = _roomService.GetRoom(idRoom);

        if (room == null) return NotFound($"{idRoom} not found");

        return Ok(room);
    }

    [HttpGet("building/{idBuilding}")]
    public IActionResult GetBuilding(string idBuilding)
    {
        var rooms = _roomService.GetBuilding(idBuilding);

        if (!rooms.Any())
            return NotFound($"No rooms in building {idBuilding}");

        return Ok(rooms);
    }

    [HttpPost]
    public IActionResult AddRoom(Room room)
    {
        var result = _roomService.AddRoom(room);

        if (result == 404)
            return BadRequest("Failed to add room");

        return Created();
    }

    [HttpPut]
    public IActionResult PutRoom(Room room)
    {
        var result = _roomService.PutRoom(room);

        if (result == 404)
            return NotFound($"{room.Id} not found");

        return Created();
    }

    [HttpDelete("{idRoom}")]
    public IActionResult DeleteRoom(int idRoom)
    {
        var result = _roomService.DeleteRoom(idRoom);

        if (result == 404) return NotFound($"{idRoom} not found");

        if (result == 409) return Conflict($"{idRoom} conlitcs");

        return NoContent();
    }
}