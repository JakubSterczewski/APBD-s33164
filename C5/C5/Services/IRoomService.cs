using C5.Models;

namespace C5.Services;

public interface IRoomService
{
    IEnumerable<Room> GetAll();
    Room? GetRoom(int idRoom);
    IEnumerable<Room> GetBuilding(string idBuilding);
    IEnumerable<Room> GetRoom(int minCapacity, bool hasProjector, bool activeOnly);
    int AddRoom(Room room);
    int PutRoom(Room room);
    int DeleteRoom(int idRoom);
}