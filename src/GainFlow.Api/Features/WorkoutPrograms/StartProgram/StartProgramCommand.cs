using GainFlow.Api.Shared.Abstractions;

namespace GainFlow.Api.Features.WorkoutPrograms.StartProgram;

public sealed record StartProgramCommand(
    string WorkoutProgramId,
    DateOnly StartDate,
    string? Notes
) : ICommand;