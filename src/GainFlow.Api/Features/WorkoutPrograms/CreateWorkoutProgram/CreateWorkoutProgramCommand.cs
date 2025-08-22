using GainFlow.Api.Shared.Abstractions;

namespace GainFlow.Api.Features.WorkoutPrograms.CreateWorkoutProgram;

public sealed record CreateWorkoutProgramCommand(
    string Name,
    string Description,
    int DurationWeeks,
    bool IsPublic
) : ICommand;