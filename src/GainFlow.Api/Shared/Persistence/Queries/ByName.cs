using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class ByName(string? exerciseName) : IDataQuery<Exercise, Exercise>
{
    public IQueryable<Exercise> Apply(IQueryable<Exercise> query)
    {
        return query.Where(ex => exerciseName == null || ex.Name.ToLower().Contains(exerciseName.ToLower()));
    }
}
