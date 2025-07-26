namespace GainFlow.Api.Features.Exercises.UpdateExercise;

public sealed record UpdateExerciseCommand(string? Name, string[]? PrimaryMuscles, string[]? SecondaryMuscles);
