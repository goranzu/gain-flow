using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Extensions;
using GainFlow.Api.Shared.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.Authentication.Login;

public sealed class LoginEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/api/login",
            async (SignInManager<IdentityUser> signInManager, LoginCommand command,
                IValidator<LoginCommand> validator,
                CancellationToken cancellationToken,
                ApplicationDbContext dbContext) =>
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

                User? user = await dbContext.Users
                    .FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken: cancellationToken);

                if (user is null)
                {
                    return Results.Problem(detail: "Invalid credentials. Please try again.",
                        statusCode: StatusCodes.Status400BadRequest);
                }

                var response = new UserResponse(user.Id, user.Email);

                return Results.Ok(response);
            });
    }
}
