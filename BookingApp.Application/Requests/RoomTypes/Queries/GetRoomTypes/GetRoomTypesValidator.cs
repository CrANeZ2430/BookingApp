using FluentValidation;

namespace BookingApp.Application.Requests.RoomTypes.Queries.GetRoomTypes;

public class GetRoomTypesValidator : AbstractValidator<GetRoomTypesQuery>
{
    public GetRoomTypesValidator()
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