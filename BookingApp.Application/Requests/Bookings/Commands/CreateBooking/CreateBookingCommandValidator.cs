using FluentValidation;

namespace BookingApp.Application.Requests.Bookings.Commands.CreateBooking;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(cbc => cbc.AttendeeCount)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Attendee count cannot be less than 1.");
        
        RuleFor(cbc => cbc.StartTime)
            .NotEmpty()
            .WithMessage("Booking start time is required.")
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Booking start time cannot be in the past.");;
        
        RuleFor(cbc => cbc.EndTime)
            .NotEmpty()
            .WithMessage("Booking end time is required.")
            .GreaterThan(cbc => cbc.StartTime)
            .WithMessage("Booking start time must be after the start time.");;
        
        RuleFor(cbc => cbc.MemberId)
            .NotEmpty()
            .WithMessage("Member ID is required.");
        
        RuleFor(cbc => cbc.RoomId)
            .NotEmpty()
            .WithMessage("Room ID is required.");
    }
}