using BookingApp.Application.Common;
using BookingApp.Application.Requests.RoomTypes.Queries.GetRoomTypes;
using BookingApp.Core.Domain.RoomTypes.Repositories;
using BookingApp.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Requests.RoomTypes.Queries;

public class GetRoomTypesQueryHandler(
    BookingAppDbContext dbContext)
    : IRequestHandler<GetRoomTypesQuery, PageResponse<GetRoomTypesDto>>
{
    public async Task<PageResponse<GetRoomTypesDto>> Handle(
        GetRoomTypesQuery request, 
        CancellationToken cancellationToken = default)
    {
        var term = request.SearchTerm?.Trim();
        
        var query = dbContext.RoomTypes
            .AsNoTracking()
            .Where(x => 
                request.SearchTerm == null || 
                x.Name.Contains(term));

        var totalCount = await query.CountAsync(cancellationToken);
        
        var roomTypes = await query
            .OrderBy(x => x.Name)
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetRoomTypesDto(
                x.RoomTypeId,
                x.Name,
                x.Description))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<GetRoomTypesDto>(
            request.Page,
            request.PageSize,
            totalCount,
            roomTypes);
    }
}