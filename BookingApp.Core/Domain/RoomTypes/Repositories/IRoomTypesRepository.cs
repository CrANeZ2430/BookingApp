using BookingApp.Core.Domain.RoomTypes.Models;

namespace BookingApp.Core.Domain.RoomTypes.Repositories;

public interface IRoomTypesRepository
{
    Task<RoomType?> GetByIdAsync(
        Guid roomTypeId, 
        CancellationToken ct = default);
    Task AddAsync(
        RoomType roomType, 
        CancellationToken ct = default);
    void Remove(
        RoomType roomType);
}