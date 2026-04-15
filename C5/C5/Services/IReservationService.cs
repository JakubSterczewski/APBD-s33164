using C5.Models;

namespace C5.Services;

public interface IReservationService
{
    IEnumerable<Reservation> GetAll();
    Reservation? GetReservation(int idReservation);
    IEnumerable<Reservation> GetReservation(DateOnly date, ReservationStatus reservationStatus, int roomId);
    int AddReservation(Reservation reservation);
    int PutReservation(Reservation reservation);
    int DeleteReservation(int reservationId);
}