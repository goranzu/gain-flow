using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Persistence.Queries.Filters;

public sealed class WorkoutProgramByNameFilter(string? search) : IDataQuery<WorkoutProgram, WorkoutProgram>
{
    public IQueryable<WorkoutProgram> Apply(IQueryable<WorkoutProgram> query)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return query;
        }

        return query.Where(wp => wp.Name.Contains(search) || wp.Description.Contains(search));
    }
}