using BookingApp.Application.Common;
using BookingApp.Core.Domain.Rooms.Repositories;
using MediatR;

namespace BookingApp.Application.Requests.Rooms.Queries.GetRooms;

public class GetRoomsQueryHandler(
    IRoomsRepository roomsRepository) 
    : IRequestHandler<GetRoomsQuery, PageResponse<RoomDto>>
{
    public async Task<PageResponse<RoomDto>> Handle(
        GetRoomsQuery request, 
        CancellationToken cancellationToken = default)
    {
        var (rooms, totalCount) = await roomsRepository.GetAsync(
            request.Page, 
            request.PageSize, 
            new RoomFilterProps(
                request.SearchTerm,
                request.MinCapability,
                request.IsAvailable),
            cancellationToken);

        var roomDtos = 
            rooms.Select(x =>
                new RoomDto(
                    x.RoomId,
                    x.Name,
                    x.Floor,
                    x.Capacity,
                    x.IsOperational,
                    new RoomTypeDto(
                        x.RoomType.Name
                        ))).ToArray();

        return new PageResponse<RoomDto>(
            request.Page,
            request.PageSize,
            totalCount,
            roomDtos);
    }
}