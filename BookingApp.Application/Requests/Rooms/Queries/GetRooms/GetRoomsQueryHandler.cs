using BookingApp.Application.Common;
using BookingApp.Core.Domain.Rooms.Repositories;
using MediatR;

namespace BookingApp.Application.Requests.Rooms.Queries.GetRooms;

public class GetRoomsQueryHandler(
    IRoomsRepository roomsRepository) 
    : IRequestHandler<GetRoomsQuery, PageResponse<GetRoomsRoomDto>>
{
    public async Task<PageResponse<GetRoomsRoomDto>> Handle(
        GetRoomsQuery request, 
        CancellationToken cancellationToken = default)
    {
        var (rooms, totalCount) = await roomsRepository.GetAsync(
            request.Page, 
            request.PageSize, 
            new RoomFilterProps(
                request.SearchTerm,
                request.MinCapability,
                request.IsOperational),
            cancellationToken);

        var roomDtos = 
            rooms.Select(x =>
                new GetRoomsRoomDto(
                    x.RoomId,
                    x.Name,
                    x.Floor,
                    x.Capacity,
                    x.IsOperational,
                    new GetRoomsRoomTypeDto(
                        x.RoomType.Name
                        ))).ToArray();

        return new PageResponse<GetRoomsRoomDto>(
            request.Page,
            request.PageSize,
            totalCount,
            roomDtos);
    }
}