using GainFlow.Api.Shared.Abstractions;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class DataQuery<TEntity> : IDataQuery<TEntity, TEntity> where TEntity : class
{
    private readonly List<IDataQuery<TEntity, TEntity>> _queries = [];

    public DataQuery<TEntity> Add(IDataQuery<TEntity, TEntity> dataQuery)
    {
        _queries.Add(dataQuery);
        return this;
    }

    public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
    {
        foreach (IDataQuery<TEntity, TEntity> queryObject in _queries)
        {
            query = queryObject.Apply(query);
        }

        return query;
    }
}
