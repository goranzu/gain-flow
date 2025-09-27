using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class OrderByCreatedAtDescending<T> : IDataQuery<T, T> where T : AuditableEntity
{
    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.OrderByDescending(entity => entity.CreatedAt);
    }
}