using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Shared;
using GainFlow.Api.Shared.Common;
using Microsoft.AspNetCore.Identity;

namespace GainFlow.Api.Features.Authentication.Login;

public sealed class LoginEndpoint : IEndpoint
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
}
