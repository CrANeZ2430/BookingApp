using FluentValidation;

namespace BookingApp.Application.Requests.Members.Queries.GetMemberById;

public class GetMemberByIdQueryValidator : AbstractValidator<GetMemberByIdQuery>
{
    public GetMemberByIdQueryValidator()
    {
        RuleFor(gmbiq => gmbiq.MemberId)
            .NotEmpty()
            .WithMessage("Member ID is required.");
    }
}