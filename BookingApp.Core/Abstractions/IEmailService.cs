namespace BookingApp.Core.Abstractions;

public interface IEmailService
{
    Task SendBookingCreationEmailAsync(
        Guid bookingId, 
        DateTime occuredAt,
        string email, 
        CancellationToken ct = default);

    Task SendMemberCreationEmailAsync(
        Guid memberId,
        DateTime occuredAt,
        string email,
        CancellationToken ct = default);
}