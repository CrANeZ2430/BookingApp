using BookingApp.Application.Common;
using MediatR;

namespace BookingApp.Application.Requests.RoomTypes.Queries.GetRoomTypes;

public record GetRoomTypesQuery(
    int Page,
    int PageSize,
    string? SearchTerm = null)
    : IRequest<PageResponse<GetRoomTypesDto>>;