using BookingApp.Core.Domain.RoomTypes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingApp.Infrastructure.Database.EntityTypeConfigurations;

public class RoomTypeEntityTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        builder.ToTable("RoomTypes");

        builder.HasKey(rt => rt.RoomTypeId);
        builder.Property(rt => rt.RoomTypeId)
            .ValueGeneratedNever();

        builder.Property(rt => rt.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(rt => rt.Description)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.HasMany(rt => rt.Rooms)
            .WithOne(r => r.RoomType)
            .HasForeignKey(r => r.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(nameof(RoomType.Rooms))
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}