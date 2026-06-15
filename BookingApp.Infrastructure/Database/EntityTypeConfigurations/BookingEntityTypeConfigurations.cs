using BookingApp.Core.Domain.Bookings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingApp.Infrastructure.Database.EntityTypeConfigurations;

public class BookingEntityTypeConfigurations : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.BookingId);
        builder.Property(b => b.BookingId)
            .ValueGeneratedNever();

        builder.Property(b => b.AttendeeCount)
            .IsRequired();

        builder.Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(b => b.StartTime)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(b => b.EndTime)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(b => b.RoomId)
            .IsRequired();
        
        builder.Property(b => b.MemberId)
            .IsRequired();

        builder.HasIndex(b => new { b.RoomId, b.StartTime, b.EndTime })
            .HasDatabaseName("IX_Bookings_Room_Timeline");

        builder.HasOne(b => b.Member)
            .WithMany()
            .HasForeignKey(b => b.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(b => b.Room)
            .WithMany()
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}