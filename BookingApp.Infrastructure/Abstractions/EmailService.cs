using BookingApp.Core.Abstractions;
using Microsoft.Extensions.Logging;

namespace BookingApp.Infrastructure.Abstractions;

public class EmailService(
    ILogger<EmailService> logger)
    : IEmailService
{
    public Task SendBookingCreationEmailAsync(
        Guid bookingId, 
        DateTime occuredAt,
        string email, 
        CancellationToken ct = default)
    {
        logger.LogInformation("[MOCK EMAIL] Confirmation for " +
                              "Booking {BookingId} at {OccuredAt} sent to {Email}", 
            bookingId, occuredAt, email);

        return Task.CompletedTask;
    }

    public Task SendMemberCreationEmailAsync(
        Guid memberId, 
        DateTime occuredAt, 
        string email,
        CancellationToken ct = default)
    {
        logger.LogInformation("[MOCK EMAIL] Confirmation for creating " +
                              "Member {MemberId} at {OccuredAt} sent to {Email}", 
            memberId, occuredAt, email);

        return Task.CompletedTask;
    }
}