using BookingApp.Core.Domain.Rooms.Repositories;
using BookingApp.Core.Domain.Rooms.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Database.Repositories.Rooms;

public class RoomsRepository(BookingAppDbContext dbContext) : IRoomsRepository
{
    public async Task<(IReadOnlyCollection<Room> Items, int TotalCount)> GetAsync(
        int page, 
        int pageSize, 
        RoomFilterProps props,
        CancellationToken ct = default)
    {
        var term = props.SearchTerm?.Trim();
        
        var query = dbContext.Rooms
                .AsNoTracking()
                .Where(x => 
                    (props.SearchTerm == null || x.Name.Contains(term)) &&
                    (props.MinCapacity == null || x.Capacity >= props.MinCapacity) &&
                    (props.IsAvailable == null || x.IsOperational == props.IsAvailable));

        var totalCount = await query.CountAsync(ct);
        
        var rooms = await query
            .Include(x => x.RoomType)
            .OrderBy(x => x.Name)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToArrayAsync(ct);

        return (rooms, totalCount);
    }

    public async Task<Room?> GetByIdAsync(
        Guid roomId, 
        CancellationToken ct = default)
    {
        return await dbContext.Rooms
            .FirstOrDefaultAsync(m => m.RoomId == roomId, ct);
    }

    public async Task AddAsync(
        Room room, 
        CancellationToken ct = default)
    {
        await dbContext.AddAsync(room, ct);
    }

    public void Remove(
        Room room)
    {
        dbContext.Remove(room);
    }
}