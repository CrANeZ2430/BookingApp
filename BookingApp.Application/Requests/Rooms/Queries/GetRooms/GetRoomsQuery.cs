using BookingApp.Application.Common;
using MediatR;

namespace BookingApp.Application.Requests.Rooms.Queries.GetRooms;

public record GetRoomsQuery(
    int Page,
    int PageSize,
    string? SearchTerm = null,
    int? MinCapacity = null,
    bool? IsOperational = null)
    : IRequest<PageResponse<GetRoomsRoomDto>>;