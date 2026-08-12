namespace BookingApp.Application.Requests.Rooms.Queries.GetRooms;

public record RoomDto(
    Guid RoomId,
    string Name,
    int Floor,
    bool IsOperational,
    RoomTypeDto RoomType);