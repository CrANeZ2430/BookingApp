using BookingApp.Core.Domain.RoomTypes.Models;

namespace BookingApp.Core.Domain.RoomTypes.Repositories;

public interface IRoomTypesRepository
{
    Task<IReadOnlyCollection<RoomType>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
    Task<RoomType?> GetByIdAsync(Guid roomTypeId, CancellationToken ct = default);
    Task AddAsync(RoomType roomType, CancellationToken ct = default);
    void Remove(RoomType roomType);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}