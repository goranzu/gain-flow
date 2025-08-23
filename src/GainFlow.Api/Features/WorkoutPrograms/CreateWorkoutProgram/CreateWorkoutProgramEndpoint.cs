using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Extensions;
using GainFlow.Api.Shared.Persistence;
using GainFlow.Api.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.WorkoutPrograms.CreateWorkoutProgram;

public sealed class CreateWorkoutProgramEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/api/workout-programs", async (
                CreateWorkoutProgramCommand command,
                CancellationToken cancellationToken,
                ApplicationDbContext context,
                IValidator<CreateWorkoutProgramCommand> validator,
                ICurrentUserService currentUserService) =>
            {
                ValidationResult? validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors.ToProblemDetailErrors();
                    return Results.ValidationProblem(errors);
                }

                string userId = await context.Users
                    .Where(u => u.IdentityId == currentUserService.UserId)
                    .Select(u => u.Id)
                    .FirstAsync(cancellationToken);

                string programId = $"wp_{Guid.CreateVersion7()}";

                WorkoutProgram workoutProgram = CreateWorkoutProgramEntity(programId, command, userId);

                await context.WorkoutPrograms.AddAsync(workoutProgram, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Results.Created($"/api/workout-programs/{programId}", new { programId });
            })
            .RequireAuthorization();
    }

    private static WorkoutProgram CreateWorkoutProgramEntity(string programId, CreateWorkoutProgramCommand command,
        string userId) => new()
    {
        Id = programId,
        Name = command.Name.Trim(),
        Description = command.Description?.Trim() ?? string.Empty,
        DurationWeeks = command.DurationWeeks,
        IsPublic = command.IsPublic,
        CreatedByUserId = userId,
        Weeks = []
    };
}
