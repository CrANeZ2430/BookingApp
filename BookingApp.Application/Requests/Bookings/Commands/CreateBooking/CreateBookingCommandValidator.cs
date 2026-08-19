using FluentValidation;

namespace BookingApp.Application.Requests.Bookings.Commands.CreateBooking;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.AttendeeCount)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Attendee count cannot be less than 1.");
        
        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("Booking start time is required.")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Booking start time cannot be in the past.");
        
        RuleFor(x => x.EndTime)
            .NotEmpty()
            .WithMessage("Booking end time is required.")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Booking end time cannot be in the past.")
            .GreaterThan(cbc => cbc.StartTime)
            .WithMessage("Booking start time must be after the start time.");
        
        RuleFor(x => x.MemberId)
            .NotEmpty()
            .WithMessage("Member ID is required.");
        
        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage("Room ID is required.");
    }
}