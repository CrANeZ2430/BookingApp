using BookingApp.Core.Domain.Bookings.Models;

namespace BookingApp.Core.Domain.Bookings.Repositories;

public interface IBookingsRepository
{
    Task<Booking?> GetByIdAsync(
        Guid bookingId, 
        CancellationToken ct = default);
    
    Task AddAsync(
        Booking booking, 
        CancellationToken ct = default);
    
    void Remove(
        Booking booking);
    
    Task<bool> HasOverlappingAsync(
        Guid roomId, 
        DateTime startTime, 
        DateTime endTime, 
        CancellationToken ct = default,
        Guid? bookingId = null);
}