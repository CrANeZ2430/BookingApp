using BookingApp.Core.Domain.Bookings.Models;
using BookingApp.Core.Domain.Bookings.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Database.Repositories.Bookings;

public class BookingsRepository(BookingAppDbContext dbContext) : IBookingsRepository
{
    public async Task<IReadOnlyCollection<Booking>> GetAsync(int page, int pageSize, CancellationToken ct = default)
    {
        return await dbContext.Bookings
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToArrayAsync(ct);
    }

    public async Task<Booking?> GetByIdAsync(Guid bookingId, CancellationToken ct = default)
    {
        return await dbContext.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == bookingId, ct);
    }

    public async Task AddAsync(Booking booking, CancellationToken ct = default)
    {
        await dbContext.Bookings.AddAsync(booking, ct);
    }

    public void Remove(Booking booking)
    {
        dbContext.Bookings.Remove(booking);
    }

    public async Task<bool> HasOverlappingAsync(Guid roomId, DateTime startTime, DateTime endTime, CancellationToken ct = default)
    {
        return await dbContext.Bookings
            .AsNoTracking()
            .AnyAsync(b => b.RoomId == roomId &&
                           b.StartTime < endTime &&
                           b.EndTime > startTime, ct);
    }
}