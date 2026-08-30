using BookingApp.Core.Domain.Rooms.Repositories;
using BookingApp.Core.Domain.Rooms.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Database.Repositories.Rooms;

public class RoomsRepository(BookingAppDbContext dbContext) : IRoomsRepository
{
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