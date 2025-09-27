using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class ExerciseById(string exerciseId) : IDataQuery<Exercise, Exercise>
{
    public IQueryable<Exercise> Apply(IQueryable<Exercise> query)
    {
        return query.Where(e => e.Id == exerciseId);
    }
}
