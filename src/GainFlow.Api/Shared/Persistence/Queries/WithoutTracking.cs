using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class WithoutTracking : IDataQuery<Exercise, Exercise>
{
    public IQueryable<Exercise> Apply(IQueryable<Exercise> query)
    {
        return query.AsNoTracking();
    }
}
