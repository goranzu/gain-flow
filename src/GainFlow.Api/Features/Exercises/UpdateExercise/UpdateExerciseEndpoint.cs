using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Extensions;
using GainFlow.Api.Shared.Persistence.Queries;

namespace GainFlow.Api.Features.Exercises.UpdateExercise;

public sealed class UpdateExerciseEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("/api/exercises/{exerciseId}", async (
                string exerciseId,
                UpdateExerciseCommand command,
                CancellationToken cancellationToken,
                IValidator<UpdateExerciseCommand> validator,
                IRepository<Exercise> exerciseRepository
            ) =>
            {
                ValidationResult? validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors.ToProblemDetailErrors();
                    return Results.ValidationProblem(errors);
                }
                DataQuery<Exercise> query = new DataQuery<Exercise>()
                    .Add(new ExerciseById(exerciseId))
                    .Add(new WithMuscleGroups());

                Exercise? exercise = await exerciseRepository.FindAsync(query, cancellationToken);

                if (exercise is null)
                {
                    return Results.NotFound();
                }

                exercise.Name = command.Name?.Trim() ?? exercise.Name;
                exercise.UpdatePrimaryMuscles(command.PrimaryMuscles);
                exercise.UpdateSecondaryMuscles(command.SecondaryMuscles);

                await exerciseRepository.SaveAsync(cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization();
    }
}
