using FluentValidation;

namespace GainFlow.Api.Features.WorkoutPrograms.ConfigureTemplate;

public sealed class ConfigureTemplateCommandValidator : AbstractValidator<ConfigureTemplateCommand>
{
    public ConfigureTemplateCommandValidator()
    {
        RuleFor(x => x.WorkoutProgramId)
            .NotEmpty();

        RuleFor(x => x.TrainingDaysPerWeek)
            .InclusiveBetween(1, 7);

        RuleFor(x => x.Days)
            .NotEmpty()
            .Must((command, days) => days.Count == command.TrainingDaysPerWeek)
            .WithMessage("Number of days must match TrainingDaysPerWeek");

        RuleForEach(x => x.Days)
            .SetValidator(new TemplateDayRequestValidator());
    }
}

public sealed class TemplateDayRequestValidator : AbstractValidator<TemplateDayRequest>
{
    public TemplateDayRequestValidator()
    {
        RuleFor(x => x.DayNumber)
            .InclusiveBetween(1, 7);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.SetGroups)
            .NotEmpty();

        RuleForEach(x => x.SetGroups)
            .SetValidator(new TemplateSetGroupRequestValidator());
    }
}

public sealed class TemplateSetGroupRequestValidator : AbstractValidator<TemplateSetGroupRequest>
{
    public TemplateSetGroupRequestValidator()
    {
        RuleFor(x => x.OrderIndex)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(type => type is "Regular" or "Superset" or "GiantSet")
            .WithMessage("Type must be Regular, Superset, or GiantSet");

        RuleFor(x => x.RestSeconds)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Notes)
            .MaximumLength(1000);

        RuleFor(x => x.Exercises)
            .NotEmpty();

        RuleForEach(x => x.Exercises)
            .SetValidator(new TemplateExerciseRequestValidator());
    }
}

public sealed class TemplateExerciseRequestValidator : AbstractValidator<TemplateExerciseRequest>
{
    public TemplateExerciseRequestValidator()
    {
        RuleFor(x => x.ExerciseId)
            .NotEmpty();

        RuleFor(x => x.OrderIndex)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.TargetSets)
            .GreaterThan(0);

        RuleFor(x => x.TargetReps)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.TargetWeight)
            .GreaterThanOrEqualTo(0)
            .When(x => x.TargetWeight.HasValue);

        RuleFor(x => x.TargetRpe)
            .MaximumLength(20);

        RuleFor(x => x.Notes)
            .MaximumLength(1000);

        RuleFor(x => x.ProgressionRule)
            .MaximumLength(500);
    }
}