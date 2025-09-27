using GainFlow.Api.Shared.Abstractions;

namespace GainFlow.Api.Shared.Persistence.Queries.Modifiers;

public sealed class PaginationModifier<T>(int page, int pageSize) : IDataQuery<T, T> where T : class
{
    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}