using BookingApp.Application.Common;
using MediatR;

namespace BookingApp.Application.Requests.Rooms.Queries.GetRooms;

public record GetRoomsQuery(
    int Page,
    int PageSize)
    : IRequest<PageResponse<RoomDto>>;