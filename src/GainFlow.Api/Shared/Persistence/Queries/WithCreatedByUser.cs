using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class WithCreatedByUser : IDataQuery<WorkoutProgram, WorkoutProgram>
{
    public IQueryable<WorkoutProgram> Apply(IQueryable<WorkoutProgram> query)
    {
        return query.Include(wp => wp.CreatedByUser);
    }
}