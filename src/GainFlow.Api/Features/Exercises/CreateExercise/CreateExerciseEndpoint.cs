using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Shared;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence;
using GainFlow.Api.Shared.Persistence.Enums;

namespace GainFlow.Api.Features.Exercises.CreateExercise;

public sealed class CreateExerciseEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/api/exercises", async (CreateExerciseCommand command,
                CancellationToken cancellationToken,
                ApplicationDbContext applicationDbContext,
                IValidator<CreateExerciseCommand> validator) =>
            {
                ValidationResult? validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors.ToProblemDetailErrors();
                    return Results.ValidationProblem(errors);
                }

                string exerciseId = $"e_{Guid.CreateVersion7()}";

                var exercise = new Exercise
                {
                    Id = exerciseId,
                    Name = command.Name.Trim(),
                    MuscleGroups = CreateMuscleGroups(command, exerciseId),
                };

                await applicationDbContext.Exercises.AddAsync(exercise, cancellationToken);
                await applicationDbContext.SaveChangesAsync(cancellationToken);

                return Results.Created($"/api/exercises/{exerciseId}", new { exerciseId });
            })
            .RequireAuthorization();
    }


    private static ExerciseMuscleGroup[] MapExerciseMuscleGroups(string[] muscleGroups,
        string exerciseId, MuscleRole role) => muscleGroups.Select(pm => new ExerciseMuscleGroup
        {
            Id = $"eg_{Guid.CreateVersion7()}",
            ExerciseId = exerciseId,
            MuscleGroup = Enum.Parse<MuscleGroup>(pm,
                ignoreCase: true),
            Role = role,
        })
        .ToArray();

    private static List<ExerciseMuscleGroup> CreateMuscleGroups(CreateExerciseCommand command,
        string exerciseId)
    {
        var muscleGroups = new List<ExerciseMuscleGroup>();
        muscleGroups.AddRange(MapExerciseMuscleGroups(command.PrimaryMuscles, exerciseId, MuscleRole.Primary));
        muscleGroups.AddRange(MapExerciseMuscleGroups(command.SecondaryMuscles, exerciseId, MuscleRole.Secondary));
        return muscleGroups;
    }
}
