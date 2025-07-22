namespace GainFlow.Api.Features.Exercises;

public sealed class GetExerciseResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> PrimaryMuscles { get; set; }
    public IEnumerable<string> SecondaryMuscles { get; set; }
}
