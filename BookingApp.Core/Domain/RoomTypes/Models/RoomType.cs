using BookingApp.Core.Domain.Rooms.Models;
using BookingApp.Core.Exceptions;

namespace BookingApp.Core.Domain.RoomTypes.Models;

public class RoomType
{
    private readonly List<Room> _rooms = new();
    
    public Guid RoomTypeId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
    
    private RoomType() {}

    private RoomType(
        string name, 
        string? description)
    {
        RoomTypeId = Guid.NewGuid();
        Name = name;
        Description = description;
    }

    public static RoomType Create(
        string name,
        string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BadRequestException("Name cannot be empty.");
        
        return new RoomType(
            name,
            description);
    }

    public void Update(
        string name,
        string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BadRequestException("Name cannot be empty.");
        
        Name = name;
        Description = description;
    }
}