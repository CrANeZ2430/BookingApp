using FluentValidation;

namespace BookingApp.Application.Requests.Members.Commands.CreateMember;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(cmc => cmc.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(100)
            .WithMessage("First name length cannot exceed 100 characters.");
        
        RuleFor(cmc => cmc.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(100)
            .WithMessage("Last name length cannot exceed 100 characters.");

        RuleFor(cmc => cmc.Role)
            .IsInEnum()
            .WithMessage("The specified role is invalid.");

        RuleFor(cmc => cmc.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .MaximumLength(256)
            .WithMessage("Email length cannot exceed 256 characters.")
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        RuleFor(cmc=> cmc.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .MaximumLength(15)
            .WithMessage("Phone number length cannot exceed 15 characters.")
            .Matches(@"^\+[1-9]\d{1,14}$")
            .WithMessage("Phone number must be a valid international format (e.g., +48123456789).");
    }
}