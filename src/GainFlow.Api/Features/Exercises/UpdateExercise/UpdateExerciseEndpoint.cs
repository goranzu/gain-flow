using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Shared;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Extensions;
using GainFlow.Api.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.Exercises.UpdateExercise;

public sealed class UpdateExerciseEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("/api/exercises/{exerciseId}", async (
                string exerciseId,
                UpdateExerciseCommand command,
                CancellationToken cancellationToken,
                ApplicationDbContext applicationDbContext,
                IValidator<UpdateExerciseCommand> validator
            ) =>
            {
                ValidationResult? validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors.ToProblemDetailErrors();
                    return Results.ValidationProblem(errors);
                }

                Exercise? exercise = await applicationDbContext.Exercises
                    .Include(e => e.MuscleGroups)
                    .FirstOrDefaultAsync(e => e.Id == exerciseId, cancellationToken: cancellationToken);

                if (exercise is null)
                {
                    return Results.NotFound();
                }

                exercise.Name = command.Name?.Trim() ?? exercise.Name;
                exercise.UpdatePrimaryMuscles(command.PrimaryMuscles);
                exercise.UpdateSecondaryMuscles(command.SecondaryMuscles);

                await applicationDbContext.SaveChangesAsync(cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization();
    }
}
