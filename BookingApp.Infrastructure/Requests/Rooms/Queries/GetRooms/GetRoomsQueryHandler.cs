using BookingApp.Application.Common;
using BookingApp.Application.Requests.Rooms.Queries.GetRooms;
using BookingApp.Core.Domain.Rooms.Repositories;
using BookingApp.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Requests.Rooms.Queries.GetRooms;

public class GetRoomsQueryHandler(
    BookingAppDbContext dbContext) 
    : IRequestHandler<GetRoomsQuery, PageResponse<GetRoomsRoomDto>>
{
    public async Task<PageResponse<GetRoomsRoomDto>> Handle(
        GetRoomsQuery request, 
        CancellationToken cancellationToken = default)
    {
        var term = request.SearchTerm?.Trim();
        
        var query = dbContext.Rooms.AsNoTracking()
            .Where(x => 
                (request.SearchTerm == null || x.Name.Contains(term)) &&
                (request.MinCapacity == null || x.Capacity >= request.MinCapacity) &&
                (request.IsOperational == null || x.IsOperational == request.IsOperational));
        
        var totalCount = await query.CountAsync(cancellationToken);
        
        var rooms = await query.Include(x => x.RoomType)
            .OrderBy(x => x.Name)
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetRoomsRoomDto(
                x.RoomId,
                x.Name,
                x.Floor,
                x.Capacity,
                x.IsOperational,
                new GetRoomsRoomTypeDto(
                    x.RoomType.Name)))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<GetRoomsRoomDto>(
            request.Page,
            request.PageSize,
            totalCount,
            rooms);
    }
}