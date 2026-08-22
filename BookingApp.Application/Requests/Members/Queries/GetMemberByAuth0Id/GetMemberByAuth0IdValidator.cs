using FluentValidation;

namespace BookingApp.Application.Requests.Members.Queries.GetMemberByAuth0Id;

public class GetMemberByAuth0IdValidator : AbstractValidator<GetMemberByAuth0IdQuery>
{
    public GetMemberByAuth0IdValidator()
    {
        RuleFor(x => x.Auth0Id)
            .NotEmpty()
            .WithMessage("Auth0 ID is required.");
    }
}