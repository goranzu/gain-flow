using GainFlow.Api.Infrastructure.Data.Entities;

namespace GainFlow.Api.Features.Exercises;

public sealed class ExerciseResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> PrimaryMuscles { get; set; }
    public IEnumerable<string> SecondaryMuscles { get; set; }
}
