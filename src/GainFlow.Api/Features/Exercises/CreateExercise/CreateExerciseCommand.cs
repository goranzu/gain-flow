namespace GainFlow.Api.Features.Exercises.CreateExercise;

public sealed record CreateExerciseCommand(
    string Name,
    string[] PrimaryMuscles,
    string[] SecondaryMuscles);
