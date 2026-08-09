namespace BookingApp.Core.Domain.Rooms.Models;

[Flags]
public enum Equipment
{
    None = 0,
    Projector = 1 << 0,
    Monitor = 1 << 1,
    WhiteBoard = 1 << 2
}