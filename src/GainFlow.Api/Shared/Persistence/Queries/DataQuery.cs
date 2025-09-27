using GainFlow.Api.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Persistence.Queries;

public sealed class DataQuery<TEntity> : IDataQuery<TEntity, TEntity> where TEntity : class
{
    private readonly List<IDataQuery<TEntity, TEntity>> _queries = [];
    private bool _withoutTracking;
    private int? _skip;
    private int? _take;

    public DataQuery<TEntity> Add(IDataQuery<TEntity, TEntity> dataQuery)
    {
        _queries.Add(dataQuery);
        return this;
    }

    public DataQuery<TEntity> WithoutTracking()
    {
        _withoutTracking = true;
        return this;
    }

    public DataQuery<TEntity> Paginate(int page, int pageSize)
    {
        _skip = (page - 1) * pageSize;
        _take = pageSize;
        return this;
    }

    public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
    {
        foreach (IDataQuery<TEntity, TEntity> queryObject in _queries)
        {
            query = queryObject.Apply(query);
        }

        if (_withoutTracking)
        {
            query = query.AsNoTracking();
        }

        if (_skip.HasValue)
        {
            query = query.Skip(_skip.Value);
        }

        if (_take.HasValue)
        {
            query = query.Take(_take.Value);
        }

        return query;
    }
}
