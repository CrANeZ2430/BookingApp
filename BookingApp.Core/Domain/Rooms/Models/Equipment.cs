namespace BookingApp.Core.Domain.Rooms.Models;

[Flags]
public enum Equipment
{
    Projector = 0,
    Monitor = 1 << 1,
    WhiteBoard = 1 << 2
}