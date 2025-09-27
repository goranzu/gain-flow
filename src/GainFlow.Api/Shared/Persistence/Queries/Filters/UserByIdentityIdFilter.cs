using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Persistence.Queries.Filters;

public sealed class UserByIdentityIdFilter(string identityId) : IDataQuery<User, User>
{
    public IQueryable<User> Apply(IQueryable<User> query)
    {
        return query.Where(u => u.IdentityId == identityId);
    }
}