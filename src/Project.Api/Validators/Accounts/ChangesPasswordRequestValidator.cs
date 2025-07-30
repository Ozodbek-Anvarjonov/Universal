using FluentValidation;
using Project.Application.Dtos.Accounts;

namespace Project.Api.Validators.Accounts;

public class ChangesPasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangesPasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(6).WithMessage("New password must be at least 6 characters.");

        RuleFor(x => x.ConfirmNewPassword)
            .Equal(x => x.NewPassword)
            .WithMessage("Confirmation password does not match the new password.");
    }
}