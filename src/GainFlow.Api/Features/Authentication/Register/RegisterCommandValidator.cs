using FluentValidation;

namespace GainFlow.Api.Features.Authentication.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(3);
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .MinimumLength(3);
        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmPassword)
            .WithMessage("Passwords do not match")
            .When(x => !string.IsNullOrEmpty(x.ConfirmPassword));
    }
}
