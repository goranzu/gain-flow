using GainFlow.Api.Shared.Abstractions;

namespace GainFlow.Api.Features.WorkoutPrograms.ConfigureTemplate;

public sealed record ConfigureTemplateCommand(
    string WorkoutProgramId,
    int TrainingDaysPerWeek,
    List<TemplateDayRequest> Days
) : ICommand;

public sealed record TemplateDayRequest(
    int DayNumber,
    string Name,
    string? Description,
    List<TemplateSetGroupRequest> SetGroups
);

public sealed record TemplateSetGroupRequest(
    int OrderIndex,
    string Type, // "Regular", "Superset", "GiantSet"
    int RestSeconds,
    string? Notes,
    List<TemplateExerciseRequest> Exercises
);

public sealed record TemplateExerciseRequest(
    string ExerciseId,
    int OrderIndex,
    int TargetSets,
    string TargetReps,
    decimal? TargetWeight,
    string? TargetRpe,
    string? Notes,
    string? ProgressionRule
);