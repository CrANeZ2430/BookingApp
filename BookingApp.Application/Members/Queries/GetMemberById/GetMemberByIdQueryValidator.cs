using FluentValidation;

namespace BookingApp.Application.Members.Queries.GetMemberById;

public class GetMemberByIdQueryValidator : AbstractValidator<GetMemberByIdQuery>
{
    public GetMemberByIdQueryValidator()
    {
        RuleFor(gmbiq => gmbiq.MemberId)
            .NotEmpty()
            .WithMessage("Member ID is required.");
    }
}