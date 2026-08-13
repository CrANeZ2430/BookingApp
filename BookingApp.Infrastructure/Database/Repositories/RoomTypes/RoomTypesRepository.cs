using BookingApp.Core.Domain.RoomTypes.Repositories;
using BookingApp.Core.Domain.RoomTypes.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Database.Repositories.RoomTypes;

public class RoomTypesRepository(BookingAppDbContext dbContext) : IRoomTypesRepository
{
    public async Task<(IReadOnlyCollection<RoomType> Items, int TotalCount)> GetAsync(
        int page, 
        int pageSize, 
        RoomTypeFilterProps props,
        CancellationToken ct = default)
    {
        var term = props.SearchTerm?.Trim();
        
        var query = dbContext.RoomTypes
            .AsNoTracking()
            .Where(x => 
                props.SearchTerm == null || 
                x.Name.Contains(term));

        var totalCount = await query.CountAsync(ct);
        
        var roomTypes = await query
            .OrderBy(x => x.Name)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToArrayAsync(ct);

        return (roomTypes, totalCount);
    }

    public async Task<RoomType?> GetByIdAsync(
        Guid roomTypeId, 
        CancellationToken ct = default)
    {
        return await dbContext.RoomTypes
            .FirstOrDefaultAsync(rt => rt.RoomTypeId == roomTypeId, ct);
    }

    public async Task AddAsync(
        RoomType roomType, 
        CancellationToken ct = default)
    {
        await dbContext.RoomTypes.AddAsync(roomType, ct);
    }

    public void Remove(
        RoomType roomType)
    {
        dbContext.RoomTypes.Remove(roomType);
    }
}