using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Extensions;
using GainFlow.Api.Shared.Persistence;
using GainFlow.Api.Shared.Persistence.Enums;
using GainFlow.Api.Shared.Services;
using Microsoft.AspNetCore.Mvc;

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
                [FromServices] CurrentUserService currentUserService) =>
            {
                ValidationResult? validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors.ToProblemDetailErrors();
                    return Results.ValidationProblem(errors);
                }

                string currentUserId = currentUserService.UserId ?? throw new UnauthorizedAccessException();
                string programId = $"wp_{Guid.CreateVersion7()}";

                var workoutProgram = new WorkoutProgram
                {
                    Id = programId,
                    Name = command.Name.Trim(),
                    Description = command.Description?.Trim() ?? string.Empty,
                    DurationWeeks = command.DurationWeeks,
                    IsPublic = command.IsPublic,
                    CreatedByUserId = currentUserId,
                    Weeks = []
                };

                await context.WorkoutPrograms.AddAsync(workoutProgram, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Results.Created($"/api/workout-programs/{programId}", new { programId });
            })
            .RequireAuthorization();
    }
}
