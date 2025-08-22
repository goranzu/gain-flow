using FluentValidation;

namespace GainFlow.Api.Features.WorkoutPrograms.CreateWorkoutProgram;

public sealed class CreateWorkoutProgramCommandValidator : AbstractValidator<CreateWorkoutProgramCommand>
{
    public CreateWorkoutProgramCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.DurationWeeks)
            .GreaterThan(0)
            .LessThanOrEqualTo(52);
    }
}