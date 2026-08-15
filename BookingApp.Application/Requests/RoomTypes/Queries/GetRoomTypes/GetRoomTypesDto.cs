namespace BookingApp.Application.Requests.RoomTypes.Queries.GetRoomTypes;

public record GetRoomTypesDto(
    Guid RoomTypeId,
    string Name,
    string Description);