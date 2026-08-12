using FluentValidation;

namespace BookingApp.Application.Requests.Rooms.Queries.GetRooms;

public class GetRoomsValidator : AbstractValidator<GetRoomsQuery>
{
    public GetRoomsValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Page number cannot be less than 0.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page size cannot be less than 1.")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size cannot exceed 100 items.");
    }
}