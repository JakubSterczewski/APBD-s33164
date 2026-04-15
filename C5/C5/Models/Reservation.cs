namespace C5.Models;

public class Reservation
{
    public Reservation(int id, int roomId, string organizerName, string topic,
        DateOnly date, TimeOnly startTime, TimeOnly endTime, ReservationStatus status)
    {
        Id = id;
        RoomId = roomId;
        OrganizerName = organizerName;
        Topic = topic;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Status = status;
    }

    public int Id { get; set; }
    public int RoomId { get; set; }
    public string OrganizerName { get; set; }
    public string Topic { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public ReservationStatus Status { get; set; }
}