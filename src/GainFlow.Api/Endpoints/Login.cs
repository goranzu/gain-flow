using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Common;
using Microsoft.AspNetCore.Identity;

namespace GainFlow.Api.Endpoints;

public sealed class Login : IEndpoint
{
    public void AddPoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/api/login",
            async (SignInManager<IdentityUser> signInManager, LoginCommand command,
                IValidator<LoginCommand> validator,
                CancellationToken cancellationToken) =>
            {
                ValidationResult? validationResult = await validator.ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors.ToProblemDetailErrors();
                    return Results.ValidationProblem(errors);
                }

                SignInResult result =
                    await signInManager.PasswordSignInAsync(command.Email, command.Password, true, false);

                if (!result.Succeeded)
                {
                    return Results.Problem(detail: "Invalid credentials. Please try again.",
                        statusCode: StatusCodes.Status400BadRequest);
                }

                return Results.NoContent();
            });
    }

    public sealed record LoginCommand(string Email, string Password);

    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(3);
        }
    }
}
