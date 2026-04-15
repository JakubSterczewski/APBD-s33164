using C5.Models;

namespace C5.Services;

public class RoomService : IRoomService
{
    public static List<Room> _rooms = new()
    {
        new Room(1, "Lab 2041", "A", 2, 10, true, true),
        new Room(2, "Lab 2042", "B", 2, 24, true, true),
        new Room(3, "Lab 2043", "C", 1, 32, false, true),
        new Room(4, "Lab 2044", "B", 2, 24, true, false),
        new Room(5, "Lab 2045", "B", 2, 24, false, false)
    };

    public IEnumerable<Room> GetAll()
    {
        return _rooms;
    }

    public Room? GetRoom(int idRoom)
    {
        return _rooms.FirstOrDefault(r => r.Id == idRoom);
    }

    public IEnumerable<Room> GetBuilding(string idBuilding)
    {
        return _rooms.Where(r => r.BuildingCode == idBuilding);
    }

    public IEnumerable<Room> GetRoom(int minCapacity, bool hasProjector, bool activeOnly)
    {
        return _rooms.Where(r =>
            r.Capacity >= minCapacity &&
            r.HasProjector == hasProjector &&
            (!activeOnly || r.IsActive));
    }

    public int AddRoom(Room room)
    {
        if (room == null) return 404;

        room.Id = _rooms.Max(r => r.Id) + 1;
        _rooms.Add(room);
        return 201;
    }

    public int PutRoom(Room room)
    {
        var existing = _rooms.FirstOrDefault(r => r.Id == room.Id);
        if (existing == null) return 404;

        existing.Name = room.Name;
        existing.BuildingCode = room.BuildingCode;
        existing.Floor = room.Floor;
        existing.Capacity = room.Capacity;
        existing.HasProjector = room.HasProjector;
        existing.IsActive = room.IsActive;

        return 200;
    }

    public int DeleteRoom(int idRoom)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == idRoom);
        if (room == null) return 404;

        var reservations = ReservationService._reservations.Where(r => r.RoomId == idRoom);

        if (reservations.Any()) return 409;

        _rooms.Remove(room);
        return 204;
    }
}