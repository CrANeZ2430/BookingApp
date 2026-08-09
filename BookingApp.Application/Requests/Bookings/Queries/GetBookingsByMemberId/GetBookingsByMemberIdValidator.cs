using FluentValidation;

namespace BookingApp.Application.Requests.Bookings.Queries.GetBookingsByMemberId;

public class GetBookingsByMemberIdValidator : AbstractValidator<GetBookingsByMemberIdQuery>
{
    public GetBookingsByMemberIdValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Page number cannot be less than 0.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page size cannot be less than 1.")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size cannot exceed 100 items.");
        
        RuleFor(x => x.MemberId)
            .NotEmpty()
            .WithMessage("Member ID is required.");
    }
}