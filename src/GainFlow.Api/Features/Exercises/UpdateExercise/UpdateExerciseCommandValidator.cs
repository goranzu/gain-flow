using FluentValidation;
using GainFlow.Api.Shared.Persistence.Enums;

namespace GainFlow.Api.Features.Exercises.UpdateExercise;

public sealed class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
{
    public UpdateExerciseCommandValidator()
    {
        RuleFor(x => x.Name)
            .Length(2, 100)
            .WithMessage("Exercise name must be between 2 and 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.PrimaryMuscles)
            .NotEmpty()
            .WithMessage("At least one primary muscle group is required when updating primary muscles.")
            .When(x => x.PrimaryMuscles != null);

        RuleFor(x => x.PrimaryMuscles)
            .Must(muscles =>
                muscles!.All(m => !string.IsNullOrWhiteSpace(m) && Enum.IsDefined(typeof(MuscleGroup), m)))
            .WithMessage("All primary muscle groups must be valid.")
            .When(x => x.PrimaryMuscles != null);

        RuleFor(x => x.SecondaryMuscles)
            .Must(muscles =>
                muscles!.All(m => !string.IsNullOrWhiteSpace(m) && Enum.IsDefined(typeof(MuscleGroup), m)))
            .WithMessage("All secondary muscle groups must be valid.")
            .When(x => x.SecondaryMuscles != null);

        RuleFor(x => x.PrimaryMuscles)
            .Must((command, primaryMuscles) =>
                command.SecondaryMuscles == null ||
                !primaryMuscles!.Intersect(command.SecondaryMuscles).Any())
            .WithMessage("Primary and secondary muscle groups cannot overlap.")
            .When(x => x.PrimaryMuscles != null && x.SecondaryMuscles != null);

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Name) ||
                       x.PrimaryMuscles != null ||
                       x.SecondaryMuscles != null)
            .WithMessage("At least one field must be provided for update.")
            .WithName("UpdateFields");
    }
}
