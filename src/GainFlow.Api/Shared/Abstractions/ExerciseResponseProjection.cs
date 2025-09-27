using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Abstractions;

public sealed class ExerciseResponseProjection : IQueryObject<Exercise, ExerciseResponse>
{
    public IQueryable<ExerciseResponse> Apply(IQueryable<Exercise> query)
    {
        return query.Select(ex => new ExerciseResponse()
        {
            Id = ex.Id,
            Name = ex.Name,
            PrimaryMuscles = ex.PrimaryMuscles.ToArray(),
            SecondaryMuscles = ex.SecondaryMuscles.ToArray()
        });
    }
}
