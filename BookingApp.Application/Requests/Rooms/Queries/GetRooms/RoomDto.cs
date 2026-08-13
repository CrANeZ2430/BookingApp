namespace BookingApp.Application.Requests.Rooms.Queries.GetRooms;

public record RoomDto(
    Guid RoomId,
    string Name,
    int Floor,
    int Capacity,
    bool IsOperational,
    RoomTypeDto RoomType);