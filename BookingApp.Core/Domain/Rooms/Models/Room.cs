using BookingApp.Core.Domain.Bookings.Models;
using BookingApp.Core.Domain.RoomTypes.Models;
using BookingApp.Core.Exceptions;

namespace BookingApp.Core.Domain.Rooms.Models;

public class Room
{
    private readonly List<Booking> _bookings = new();
    
    public Guid RoomId { get; private set; }
    public string Name { get; private set; }
    public int Floor { get; private set; }
    public int Capacity { get; private set; }
    public Equipment Equipment { get; private set; }
    public bool IsOperational { get; private set; }
    
    public Guid RoomTypeId { get; private set; }
    public RoomType RoomType { get; private set; }
    public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();
    
    private Room() {}

    private Room(
        string name,
        int floor,
        int capacity,
        Equipment equipment,
        bool isOperational,
        Guid roomTypeId)
    {
        RoomId = Guid.NewGuid();
        Name = name;
        Floor = floor;
        Capacity = capacity;
        Equipment = equipment;
        IsOperational = isOperational;
        RoomTypeId = roomTypeId;
    }

    public static Room Create(
        string name,
        int floor,
        int capacity,
        Equipment equipment,
        bool isOperational,
        Guid roomTypeId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BadRequestException("Room name cannot be empty.");

        if (capacity <= 0)
            throw new BadRequestException("Room capacity must be greater than zero.");
        
        if (floor < 0)
            throw new BadRequestException("Floor cannot be negative.");
        
        return new Room(
            name,
            floor,
            capacity,
            equipment,
            isOperational,
            roomTypeId);
    }

    public void Update(
        string name,
        int floor,
        int capacity,
        Equipment equipment,
        bool isOperational,
        Guid roomTypeId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BadRequestException("Room name cannot be empty.");

        if (capacity <= 0)
            throw new BadRequestException("Room capacity must be greater than zero.");
        
        if (floor < 0)
            throw new BadRequestException("Floor cannot be negative.");
        
        Name = name;
        Floor = floor;
        Capacity = capacity;
        Equipment = equipment;
        IsOperational = isOperational;
        RoomTypeId = roomTypeId;
    }
    
    public void CloseForMaintenance()
    {
        if (!IsOperational)
            throw new BadRequestException("Room is already closed for maintenance.");

        IsOperational = false;
    }

    public void OpenForUse()
    {
        if (IsOperational)
            throw new BadRequestException("Room is already open and operational.");

        IsOperational = true;
    }
}