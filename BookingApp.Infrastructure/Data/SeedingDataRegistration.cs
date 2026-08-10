using BookingApp.Core.Domain.Members.Models;
using BookingApp.Core.Domain.Rooms.Models;
using BookingApp.Core.Domain.RoomTypes.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Data;

public static class SeedingDataRegistration
{
    public static DbContextOptionsBuilder AddSeedingData(this DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseAsyncSeeding(async (context, _, ct) =>
                {
                    if (!await context.Set<RoomType>().AnyAsync(ct))
                    {
                        var roomTypes = CreateRoomTypes();
                        
                        await context.Set<RoomType>().AddRangeAsync(roomTypes, ct);
                        await context.SaveChangesAsync(ct);
                    }

                    if (!await context.Set<Room>().AnyAsync(ct))
                    {
                        var rooms = await CreateRoomsAsync(context, ct);
                            
                        await context.Set<Room>().AddRangeAsync(rooms, ct);
                        await context.SaveChangesAsync(ct);   
                    }
                });

        optionsBuilder
            .UseSeeding((context, _) =>
            {
                if (!context.Set<RoomType>().Any())
                {
                    var roomTypes = CreateRoomTypes();
                        
                    context.Set<RoomType>().AddRange(roomTypes);
                    context.SaveChanges();
                }

                if (!context.Set<Room>().Any())
                {
                    var rooms = CreateRooms(context);
                            
                    context.Set<Room>().AddRange(rooms);
                    context.SaveChanges();   
                }
            });

        return optionsBuilder;
    }

    private static RoomType[] CreateRoomTypes()
    {
        return new[]
            {
                RoomType.Create(
                    "Standard Conference Room", 
                    "Ideal for team syncs, interviews, and small group meetings."),
                RoomType.Create(
                    "Executive Boardroom",
                    "High-end space tailored for stakeholder presentations and executive meetings."),
                RoomType.Create(
                    "Focus Hub",
                    "Quiet environment designed for individual deep work or pairing sessions.")
            };
    }

    private static async Task<Room[]> CreateRoomsAsync(
        DbContext context,
        CancellationToken ct = default)
    {
        var conferenceRoom = await context.Set<RoomType>()
            .FirstOrDefaultAsync(x => x.Name == "Standard Conference Room", ct);
        var boardroom = await context.Set<RoomType>()
            .FirstOrDefaultAsync(x => x.Name == "Executive Boardroom", ct);
        var focusHub = await context.Set<RoomType>()
            .FirstOrDefaultAsync(x => x.Name == "Focus Hub", ct);

        return new[]
            {
                Room.Create(
                    "Turing Room 101",
                    1,
                    8,
                    Equipment.WhiteBoard | Equipment.Monitor,
                    true,
                    conferenceRoom.RoomTypeId),
                Room.Create(
                    "Executive Suite 201",
                    2,
                    16,
                    Equipment.WhiteBoard | Equipment.Projector,
                    true,
                    boardroom.RoomTypeId),
                Room.Create(
                    "Pod A-3",
                    3,
                    2,
                    Equipment.Monitor,
                    true,
                    focusHub.RoomTypeId)
            };
    }

    private static Room[] CreateRooms(DbContext context)
    {
        var conferenceRoom = context.Set<RoomType>()
            .FirstOrDefault(x => x.Name == "Standard Conference Room");
        var boardroom = context.Set<RoomType>()
            .FirstOrDefault(x => x.Name == "Executive Boardroom");
        var focusHub = context.Set<RoomType>()
            .FirstOrDefault(x => x.Name == "Focus Hub");

        return new[]
            {
                Room.Create(
                    "Turing Room 101",
                    1,
                    8,
                    Equipment.WhiteBoard | Equipment.Monitor,
                    true,
                    conferenceRoom.RoomTypeId),
                Room.Create(
                    "Executive Suite 201",
                    2,
                    16,
                    Equipment.WhiteBoard | Equipment.Projector,
                    true,
                    boardroom.RoomTypeId),
                Room.Create(
                    "Pod A-3",
                    3,
                    2,
                    Equipment.Monitor,
                    true,
                    focusHub.RoomTypeId)
            };
    }
}