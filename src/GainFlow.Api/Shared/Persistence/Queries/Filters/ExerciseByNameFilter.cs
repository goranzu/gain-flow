using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Persistence.Queries.Filters;

public sealed class ExerciseByNameFilter(string? search) : IDataQuery<Exercise, Exercise>
{
    public IQueryable<Exercise> Apply(IQueryable<Exercise> query)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return query;
        }

        return query.Where(e => e.Name.Contains(search));
    }
}