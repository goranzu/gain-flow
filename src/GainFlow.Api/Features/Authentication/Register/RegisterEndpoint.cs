using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Shared;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GainFlow.Api.Features.Authentication.Register;

public sealed class RegisterEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/api/register", async (IdentityApplicationDbContext identityApplicationDbContext,
            ApplicationDbContext applicationDbContext,
            CancellationToken cancellationToken,
            UserManager<IdentityUser> userManager,
            RegisterCommand command,
            IValidator<RegisterCommand> validator) =>
        {
            ValidationResult? validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.ToProblemDetailErrors();
                return Results.ValidationProblem(errors);
            }

            await using IDbContextTransaction transaction =
                await applicationDbContext.Database.BeginTransactionAsync(cancellationToken);
            identityApplicationDbContext.Database.SetDbConnection(applicationDbContext.Database.GetDbConnection());
            await identityApplicationDbContext.Database.UseTransactionAsync(transaction.GetDbTransaction(),
                cancellationToken);

            var identityUser = new IdentityUser { Email = command.Email, UserName = command.Email };

            IdentityResult identityResult = await userManager.CreateAsync(identityUser, command.Password);

            if (!identityResult.Succeeded)
            {
                return Results.Problem(
                    detail: "Unable to register user, please try again",
                    extensions: identityResult.ToErrorsDictionary(),
                    statusCode: StatusCodes.Status400BadRequest);
            }

            var user = new User
            {
                Id = $"u_{Guid.CreateVersion7()}",
                Email = command.Email,
                CreatedAt = DateTimeOffset.UtcNow,
                IdentityId = identityUser.Id
            };

            applicationDbContext.Users.Add(user);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Results.NoContent();
        });
    }
}
