using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Persistence.Queries.Filters;

public sealed class WorkoutProgramByPublicFilter(bool isPublic) : IDataQuery<WorkoutProgram, WorkoutProgram>
{
    public IQueryable<WorkoutProgram> Apply(IQueryable<WorkoutProgram> query)
    {
        return query.Where(wp => wp.IsPublic == isPublic);
    }
}