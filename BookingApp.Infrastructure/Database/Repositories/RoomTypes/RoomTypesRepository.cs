using BookingApp.Core.Domain.RoomTypes.Models;
using BookingApp.Core.Domain.RoomTypes.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Database.Repositories.RoomTypes;

public class RoomTypesRepository(BookingAppDbContext dbContext) : IRoomTypesRepository
{
    public async Task<IReadOnlyCollection<RoomType>> GetAsync(int page, int pageSize, CancellationToken ct = default)
    {
        return await dbContext.RoomTypes
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToArrayAsync(ct);
    }

    public async Task<RoomType?> GetByIdAsync(Guid roomTypeId, CancellationToken ct = default)
    {
        return await dbContext.RoomTypes
            .FirstOrDefaultAsync(rt => rt.RoomTypeId == roomTypeId, ct);
    }

    public async Task AddAsync(RoomType roomType, CancellationToken ct = default)
    {
        await dbContext.RoomTypes.AddAsync(roomType, ct);
    }

    public void Remove(RoomType roomType)
    {
        dbContext.RoomTypes.Remove(roomType);
    }
}