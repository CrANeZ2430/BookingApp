namespace BookingApp.Application.Requests.Rooms.Queries.GetRooms;

public record GetRoomsRoomDto(
    Guid RoomId,
    string Name,
    int Floor,
    int Capacity,
    bool IsOperational,
    GetRoomsRoomTypeDto RoomType);