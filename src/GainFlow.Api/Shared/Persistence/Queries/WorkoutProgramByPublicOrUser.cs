using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class WorkoutProgramByPublicOrUser(string userId) : IDataQuery<WorkoutProgram, WorkoutProgram>
{
    public IQueryable<WorkoutProgram> Apply(IQueryable<WorkoutProgram> query)
    {
        return query.Where(wp => wp.IsPublic || wp.CreatedByUserId == userId);
    }
}
