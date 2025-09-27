using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Persistence.Queries.Includes;

public sealed class WithCreatedByUserInclude : IDataQuery<WorkoutProgram, WorkoutProgram>
{
    public IQueryable<WorkoutProgram> Apply(IQueryable<WorkoutProgram> query)
    {
        return query.Include(wp => wp.CreatedByUser);
    }
}