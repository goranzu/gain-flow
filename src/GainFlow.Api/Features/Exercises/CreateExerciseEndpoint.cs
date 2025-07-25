using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Shared;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence;
using GainFlow.Api.Shared.Persistence.Enums;

namespace GainFlow.Api.Features.Exercises;

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
        });
    }

    public sealed record CreateExerciseCommand(
        string Name,
        string[] PrimaryMuscles,
        string[] SecondaryMuscles);

    public sealed class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Exercise name is required.")
                .Length(2, 100)
                .WithMessage("Exercise name must be between 2 and 100 characters.");

            RuleFor(x => x.PrimaryMuscles)
                .NotEmpty()
                .WithMessage("At least one primary muscle group is required.")
                .Must(muscles => muscles.All(m => Enum.IsDefined(typeof(MuscleGroup), m)))
                .WithMessage("Invalid muscle group specified");

            RuleFor(x => x.SecondaryMuscles)
                .Must(muscles => muscles.All(m => Enum.IsDefined(typeof(MuscleGroup), m)))
                .WithMessage("Invalid muscle group specified");

            RuleFor(x => x)
                .Must(x => !x.PrimaryMuscles.Intersect(x.SecondaryMuscles ?? []).Any())
                .WithMessage("Primary and secondary muscle groups cannot overlap.")
                .WithName("PrimaryMuscles");
        }
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

    private static List<ExerciseMuscleGroup> CreateMuscleGroups(CreateExerciseCommand command, string exerciseId)
    {
        var muscleGroups = new List<ExerciseMuscleGroup>();
        muscleGroups.AddRange(MapExerciseMuscleGroups(command.PrimaryMuscles, exerciseId, MuscleRole.Primary));
        muscleGroups.AddRange(MapExerciseMuscleGroups(command.SecondaryMuscles, exerciseId, MuscleRole.Secondary));
        return muscleGroups;
    }
}
