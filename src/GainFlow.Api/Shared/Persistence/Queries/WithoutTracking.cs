using GainFlow.Api.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class WithoutTracking<T> : IDataQuery<T, T> where T : class
{
    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.AsNoTracking();
    }
}
