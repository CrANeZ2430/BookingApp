using FluentValidation;

namespace BookingApp.Application.Requests.Members.Queries.GetMembers;

public class GetMembersValidator : AbstractValidator<GetMembersQuery>
{
    public GetMembersValidator()
    {
        RuleFor(gmq => gmq.Page)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Page number cannot be less than 0.");
        
        RuleFor(gmq => gmq.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page size cannot be less than 1.")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size cannot exceed 100 items.");
    }
}