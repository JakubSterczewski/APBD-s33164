using C5.Models;
using C5.Services;
using Microsoft.AspNetCore.Mvc;

namespace C5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public IActionResult GetReservations(DateOnly? date, ReservationStatus? status, int? roomId)
    {
        if (date == null && status == null && roomId == null)
            return Ok(_reservationService.GetAll());

        var reservations =
            _reservationService.GetReservation(date ?? DateOnly.MinValue, status ?? default, roomId ?? 0);

        if (!reservations.Any())
            return NotFound("No reservations with these filters found");

        return Ok(reservations);
    }

    [HttpGet("{idReservation}")]
    public IActionResult GetReservation(int idReservation)
    {
        var reservation = _reservationService.GetReservation(idReservation);

        if (reservation == null) return NotFound($"{idReservation} not found");

        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult AddReservation(Reservation reservation)
    {
        var result = _reservationService.AddReservation(reservation);

        if (result == 404)
            return NotFound("Failed to add reservation");
        if (result == 409) return Conflict("Conlicts");

        return Created();
    }

    [HttpPut("{idReservation}")]
    public IActionResult PutRoom(Reservation reservation)
    {
        var result = _reservationService.PutReservation(reservation);

        if (result == 404)
            return NotFound($"{reservation.Id} not found");

        return Created();
    }

    [HttpDelete("{idReservation}")]
    public IActionResult DeleteReservation(int idReservation)
    {
        var result = _reservationService.DeleteReservation(idReservation);

        if (result == 404)
            return NotFound($"{idReservation} not found");

        return NoContent();
    }
}