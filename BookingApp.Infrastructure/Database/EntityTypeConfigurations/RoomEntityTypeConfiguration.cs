using BookingApp.Core.Domain.Rooms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingApp.Infrastructure.Database.EntityTypeConfigurations;

public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(r => r.RoomId);
        builder.Property(r => r.RoomId)
            .ValueGeneratedNever();
        
        builder.Property(r => r.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(r => r.Floor)
            .IsRequired();

        builder.Property(r => r.Capacity)
            .IsRequired();

        builder.Property(r => r.Equipment)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(r => r.IsOperational)
            .IsRequired();

        builder.Property(r => r.RoomTypeId)
            .IsRequired();

        builder.HasOne(r => r.RoomType)
            .WithMany()
            .HasForeignKey(r => r.RoomTypeId);

        builder.HasMany(r => r.Bookings)
            .WithOne(b => b.Room)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Restrict);;

        builder.Navigation(nameof(Room.Bookings))
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}