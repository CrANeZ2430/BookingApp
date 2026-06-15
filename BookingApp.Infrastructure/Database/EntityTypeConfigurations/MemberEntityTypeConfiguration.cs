using BookingApp.Core.Domain.Members.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingApp.Infrastructure.Database.EntityTypeConfigurations;

public class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");
        
        builder.HasKey(m => m.MemberId);
        builder.Property(m => m.MemberId)
            .ValueGeneratedNever();

        builder.Property(m => m.FirstName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(m => m.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Role)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(m => m.Email)
            .HasMaxLength(254)
            .IsRequired();
        
        builder.Property(m => m.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();

        builder.HasMany(m => m.Bookings)
            .WithOne(b => b.Member)
            .HasForeignKey(b => b.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(nameof(Member.Bookings))
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}