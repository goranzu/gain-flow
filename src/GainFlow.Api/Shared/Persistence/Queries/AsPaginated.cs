using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class AsPaginated(int page, int pageSize) : IDataQuery<Exercise, Exercise>
{
    public IQueryable<Exercise> Apply(IQueryable<Exercise> query)
    {
        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}
