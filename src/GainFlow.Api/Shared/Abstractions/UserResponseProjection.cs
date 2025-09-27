using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Abstractions;

public sealed class UserResponseProjection : IDataQuery<User, UserResponse>
{
    public IQueryable<UserResponse> Apply(IQueryable<User> query)
    {
        return query.Select(u => new UserResponse(u.Id, u.Email));
    }
}