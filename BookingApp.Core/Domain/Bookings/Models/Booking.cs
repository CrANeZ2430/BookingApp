using BookingApp.Core.Domain.Members.Models;
using BookingApp.Core.Domain.Rooms.Models;

namespace BookingApp.Core.Domain.Bookings.Models;

public class Booking
{
    public Guid BookingId { get; private set; }
    public int AttendeeCount { get; private set; }
    public BookingStatus Status { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Guid MemberId { get; private set; }
    public Guid RoomId { get; private set; }
    public Member Member { get; private set; }
    public Room Room { get; private set; }
    
    private Booking() {}

    private Booking(
        int attendeeCount,
        DateTime startTime,
        DateTime endTime,
        Guid memberId,
        Guid roomId)
    {
        BookingId = Guid.NewGuid();
        AttendeeCount = attendeeCount;
        Status = BookingStatus.Pending;
        StartTime = startTime;
        EndTime = endTime;
        CreatedAt = DateTime.UtcNow;
        MemberId = memberId;
        RoomId = roomId;
    }

    public static Booking Create(
        int attendeeCount,
        DateTime startTime,
        DateTime endTime,
        Guid memberId,
        Guid roomId)
    {
        if (attendeeCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(attendeeCount), "Room capacity must be greater than zero.");
        
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be before end time.");
        
        return new Booking(
            attendeeCount, 
            startTime, 
            endTime, 
            memberId, 
            roomId);
    }

    public void Update(
        int attendeeCount,
        DateTime startTime,
        DateTime endTime,
        Guid roomId)
    {
        if (attendeeCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(attendeeCount), "Room capacity must be greater than zero.");
        
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be before end time.");
        
        AttendeeCount = attendeeCount;
        StartTime = startTime;
        EndTime = endTime;
        RoomId = roomId;
    }
    
    public void Confirm()
    {
        if (Status != BookingStatus.Pending)
            throw new InvalidOperationException("Only pending bookings can be confirmed.");
        
        Status = BookingStatus.Confirmed;
    }

    public void Cancel()
    {
        Status = BookingStatus.Cancelled;
    }
}