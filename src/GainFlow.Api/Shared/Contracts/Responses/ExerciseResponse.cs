using System.Linq.Expressions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Contracts.Responses;

public sealed class ExerciseResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> PrimaryMuscles { get; set; }
    public IEnumerable<string> SecondaryMuscles { get; set; }

    public static Expression<Func<Exercise, ExerciseResponse>> Projection()
    {
        return ex => new ExerciseResponse
        {
            Id = ex.Id,
            Name = ex.Name,
            PrimaryMuscles = ex.PrimaryMuscles.ToArray(),
            SecondaryMuscles = ex.SecondaryMuscles.ToArray()
        };
    }

    public static ExerciseResponse FromExercise(Exercise exercise)
        => new()
        {
            Id = exercise.Id,
            Name = exercise.Name,
            PrimaryMuscles = exercise.PrimaryMuscles.ToArray(),
            SecondaryMuscles = exercise.SecondaryMuscles.ToArray()
        };
}
