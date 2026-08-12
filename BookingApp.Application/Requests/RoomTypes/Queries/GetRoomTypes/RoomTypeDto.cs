namespace BookingApp.Application.Requests.RoomTypes.Queries.GetRoomTypes;

public record RoomTypeDto(
    Guid RoomTypeId,
    string Name,
    string Description);