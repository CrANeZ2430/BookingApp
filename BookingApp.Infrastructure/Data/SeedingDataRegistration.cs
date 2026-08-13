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
                    "Quiet environment designed for individual deep work or pairing sessions."),
                RoomType.Create(
                    "Auditorium & Event Space",
                    "Large capacity room equipped for company town halls, workshops, and keynotes."),
                RoomType.Create(
                    "Creative Lab",
                    "Flexible workshop space equipped for design sprints, brainstorming, and hardware demo sessions.")
            };
    }

    private static async Task<Room[]> CreateRoomsAsync(
        DbContext context,
        CancellationToken ct = default)
    {
        var conference = await context.Set<RoomType>()
            .FirstOrDefaultAsync(x => x.Name == "Standard Conference Room", ct);
        var boardroom = await context.Set<RoomType>()
            .FirstOrDefaultAsync(x => x.Name == "Executive Boardroom",ct);
        var focus = await context.Set<RoomType>()
            .FirstOrDefaultAsync(x => x.Name == "Focus Hub", ct);
        var auditorium = await context.Set<RoomType>()
            .FirstOrDefaultAsync(x => x.Name == "Auditorium & Event Space", ct);
        var lab = await context.Set<RoomType>()
            .FirstOrDefaultAsync(x => x.Name == "Creative Lab", ct);

        return new[]
            {
                Room.Create(
                    "Turing Room 101", 
                    1, 
                    8, 
                    Equipment.WhiteBoard | Equipment.Monitor, 
                    true, 
                    conference.RoomTypeId),
                Room.Create(
                    "Lovelace Auditorium 102",
                    1,
                    50,
                    Equipment.Projector | Equipment.Monitor,
                    true,
                    auditorium.RoomTypeId),
                Room.Create(
                    "Babbage Sync 103",
                    1,
                    6,
                    Equipment.WhiteBoard | Equipment.Monitor,
                    true,
                    conference.RoomTypeId),
                Room.Create(
                    "Executive Suite 201",
                    2,
                    16,
                    Equipment.WhiteBoard | Equipment.Projector,
                    true,
                    boardroom.RoomTypeId),
                Room.Create(
                    "Hamilton Lab 202",
                    2,
                    12,
                    Equipment.WhiteBoard | Equipment.Monitor | Equipment.Projector,
                    true,
                    lab.RoomTypeId),
                Room.Create(
                    "Hopper Boardroom 203",
                    2,
                    20,
                    Equipment.WhiteBoard | Equipment.Projector,
                    true,
                    boardroom.RoomTypeId),
                Room.Create(
                    "Pod A-301",
                    3,
                    2,
                    Equipment.Monitor,
                    true,
                    focus.RoomTypeId),
                Room.Create(
                    "Pod B-302",
                    3,
                    2,
                    Equipment.Monitor,
                    true,
                    focus.RoomTypeId),
                Room.Create(
                    "Knuth Workshop 303",
                    3,
                    10,
                    Equipment.WhiteBoard | Equipment.Monitor,
                    true,
                    conference.RoomTypeId),
                Room.Create(
                    "Pod C-401",
                    4,
                    1,
                    Equipment.Monitor,
                    true,
                    focus.RoomTypeId),
                Room.Create(
                    "Ritchie Suite 402",
                    4,
                    8,
                    Equipment.WhiteBoard,
                    false,
                    conference.RoomTypeId)
            };
    }

    private static Room[] CreateRooms(DbContext context)
    {
        var conference = context.Set<RoomType>()
            .FirstOrDefault(x => x.Name == "Standard Conference Room");
        var boardroom = context.Set<RoomType>()
            .FirstOrDefault(x => x.Name == "Executive Boardroom");
        var focus = context.Set<RoomType>()
            .FirstOrDefault(x => x.Name == "Focus Hub");
        var auditorium = context.Set<RoomType>()
            .FirstOrDefault(x => x.Name == "Auditorium & Event Space");
        var lab = context.Set<RoomType>()
            .FirstOrDefault(x => x.Name == "Creative Lab");

        return new[]
            { 
                Room.Create(
                    "Turing Room 101", 
                    1, 
                    8, 
                    Equipment.WhiteBoard | Equipment.Monitor, 
                    true, 
                    conference.RoomTypeId),
                Room.Create(
                    "Lovelace Auditorium 102",
                    1,
                    50,
                    Equipment.Projector | Equipment.Monitor,
                    true,
                    auditorium.RoomTypeId),
                Room.Create(
                    "Babbage Sync 103",
                    1,
                    6,
                    Equipment.WhiteBoard | Equipment.Monitor,
                    true,
                    conference.RoomTypeId),
                Room.Create(
                    "Executive Suite 201",
                    2,
                    16,
                    Equipment.WhiteBoard | Equipment.Projector,
                    true,
                    boardroom.RoomTypeId),
                Room.Create(
                    "Hamilton Lab 202",
                    2,
                    12,
                    Equipment.WhiteBoard | Equipment.Monitor | Equipment.Projector,
                    true,
                    lab.RoomTypeId),
                Room.Create(
                    "Hopper Boardroom 203",
                    2,
                    20,
                    Equipment.WhiteBoard | Equipment.Projector,
                    true,
                    boardroom.RoomTypeId),
                Room.Create(
                    "Pod A-301",
                    3,
                    2,
                    Equipment.Monitor,
                    true,
                    focus.RoomTypeId),
                Room.Create(
                    "Pod B-302",
                    3,
                    2,
                    Equipment.Monitor,
                    true,
                    focus.RoomTypeId),
                Room.Create(
                    "Knuth Workshop 303",
                    3,
                    10,
                    Equipment.WhiteBoard | Equipment.Monitor,
                    true,
                    conference.RoomTypeId),
                Room.Create(
                    "Pod C-401",
                    4,
                    1,
                    Equipment.Monitor,
                    true,
                    focus.RoomTypeId),
                Room.Create(
                    "Ritchie Suite 402",
                    4,
                    8,
                    Equipment.WhiteBoard,
                    false,
                    conference.RoomTypeId)
            };
    }
}