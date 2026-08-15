using BookingApp.Application.Common;
using BookingApp.Core.Domain.RoomTypes.Repositories;
using MediatR;

namespace BookingApp.Application.Requests.RoomTypes.Queries.GetRoomTypes;

public class GetRoomTypesQueryHandler(
    IRoomTypesRepository roomTypesRepository)
    : IRequestHandler<GetRoomTypesQuery, PageResponse<GetRoomTypesDto>>
{
    public async Task<PageResponse<GetRoomTypesDto>> Handle(
        GetRoomTypesQuery request, 
        CancellationToken cancellationToken = default)
    {
        var (roomTypes, totalCount) = 
            await roomTypesRepository.GetAsync(
                request.Page, 
                request.PageSize, 
                new RoomTypeFilterProps(
                    request.SearchTerm),
                cancellationToken);

        var roomTypeDtos = roomTypes.Select(x => 
            new GetRoomTypesDto(
                x.RoomTypeId,
                x.Name, 
                x.Description))
            .ToArray();

        return new PageResponse<GetRoomTypesDto>(
            request.Page,
            request.PageSize,
            totalCount,
            roomTypeDtos);
    }
}