using FluentValidation;

namespace GainFlow.Api.Features.WorkoutPrograms.StartProgram;

public sealed class StartProgramCommandValidator : AbstractValidator<StartProgramCommand>
{
    public StartProgramCommandValidator()
    {
        RuleFor(x => x.WorkoutProgramId)
            .NotEmpty();

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today.AddDays(-7)))
            .WithMessage("Start date cannot be more than 7 days in the past");

        RuleFor(x => x.Notes)
            .MaximumLength(1000);
    }
}