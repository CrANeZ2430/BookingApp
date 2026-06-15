using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.Models;
using BookingApp.Core.Domain.Members.Models;
using BookingApp.Core.Domain.Rooms.Models;
using BookingApp.Core.Domain.RoomTypes.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Database;

public class BookingAppDbContext(DbContextOptions<BookingAppDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Member> Members { get; private set; }
    public DbSet<Room> Rooms { get; private set; }
    public DbSet<RoomType> RoomTypes { get; private set; }
    public DbSet<Booking> Bookings { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingAppDbContext).Assembly);
        modelBuilder.HasDefaultSchema("BookingApp");
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }
}