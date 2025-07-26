using FluentValidation;
using GainFlow.Api.Shared.Persistence.Enums;

namespace GainFlow.Api.Features.Exercises.CreateExercise;

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
