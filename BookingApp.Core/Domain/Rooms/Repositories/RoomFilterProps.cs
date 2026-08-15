namespace BookingApp.Core.Domain.Rooms.Repositories;

public record RoomFilterProps(
    string? SearchTerm = null,
    int? MinCapacity = null,
    bool? IsOperational = null);