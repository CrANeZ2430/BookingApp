using BookingApp.Core.Domain.Rooms.Models;

namespace BookingApp.Core.Domain.Rooms.Repositories;

public interface IRoomsRepository
{
    Task<Room?> GetByIdAsync(
        Guid roomId, 
        CancellationToken ct = default);
    Task AddAsync(
        Room room, 
        CancellationToken ct = default);
    void Remove(
        Room room);
}