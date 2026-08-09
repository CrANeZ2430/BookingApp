using FluentValidation;

namespace BookingApp.Application.Requests.Members.Commands.DeleteMember;

public class DeleteMemberValidator : AbstractValidator<DeleteMemberCommand>
{
    public DeleteMemberValidator()
    {
        RuleFor(x => x.MemberId)
            .NotEmpty()
            .WithMessage("Member ID is required.");
    }
}