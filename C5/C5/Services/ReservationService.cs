using C5.Models;

namespace C5.Services;

public class ReservationService : IReservationService
{
    public static List<Reservation> _reservations = new()
    {
        new Reservation(1, 2, "Anna Kowalska", "Warsztaty z HTTP i REST", new DateOnly(2026, 05, 10),
            new TimeOnly(12, 0, 0), new TimeOnly(16, 0, 0), ReservationStatus.CONFIRMED),
        new Reservation(2, 2, "Anna Kowalska1", "Warsztaty z HTTP i REST", new DateOnly(2026, 05, 10),
            new TimeOnly(14, 0, 0), new TimeOnly(16, 0, 0), ReservationStatus.PLANNED),
        new Reservation(3, 2, "Anna Kowalska2", "Warsztaty z HTTP i REST1", new DateOnly(2026, 05, 10),
            new TimeOnly(12, 5, 0), new TimeOnly(14, 0, 0), ReservationStatus.CONFIRMED),
        new Reservation(4, 2, "Anna Kowalska3", "Warsztaty z HTTP i REST22", new DateOnly(2026, 05, 10),
            new TimeOnly(18, 0, 0), new TimeOnly(22, 0, 0), ReservationStatus.PLANNED),
        new Reservation(5, 2, "Anna Kowalska4", "Warsztaty z HTTP i REST", new DateOnly(2026, 05, 10),
            new TimeOnly(12, 03, 0), new TimeOnly(14, 0, 0), ReservationStatus.CONFIRMED),
        new Reservation(6, 2, "Anna Kowalska", "Warsztaty z HTTP i REST312", new DateOnly(2026, 05, 10),
            new TimeOnly(20, 2, 0), new TimeOnly(23, 0, 0), ReservationStatus.CONFIRMED)
    };

    private readonly IRoomService _roomService;

    public ReservationService(IRoomService roomService)
    {
        _roomService = roomService;
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _reservations;
    }

    public Reservation? GetReservation(int idReservation)
    {
        return _reservations.FirstOrDefault(r => r.Id == idReservation);
    }

    public IEnumerable<Reservation> GetReservation(DateOnly date, ReservationStatus reservationStatus, int roomId)
    {
        return _reservations.Where(r => r.Date == date && r.Status == reservationStatus && r.RoomId == roomId);
    }

    public int AddReservation(Reservation reservation)
    {
        var room = _roomService.GetRoom(reservation.RoomId);

        if (room == null || !room.IsActive) return 404;

        var reservations = _reservations
            .Where(r =>
                r.Date == reservation.Date &&
                r.StartTime < reservation.EndTime &&
                r.EndTime > reservation.StartTime
            );

        if (!reservations.Any()) return 409;

        reservation.Id = _reservations.Max(r => r.Id) + 1;
        _reservations.Add(reservation);
        return 201;
    }

    public int PutReservation(Reservation reservation)
    {
        var existing = _reservations.FirstOrDefault(r => r.Id == reservation.Id);
        if (existing == null) return 404;

        existing.Id = reservation.Id;
        existing.RoomId = reservation.RoomId;
        existing.Date = reservation.Date;
        existing.EndTime = reservation.EndTime;
        existing.StartTime = reservation.StartTime;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Status = reservation.Status;

        return 200;
    }

    public int DeleteReservation(int reservationId)
    {
        var reservation = _reservations.FirstOrDefault(r => r.Id == reservationId);
        if (reservation == null) return 404;

        _reservations.Remove(reservation);
        return 204;
    }
}