using GainFlow.Api.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Persistence;

public sealed class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<TResult>> QueryAsync<TResult>(IDataQuery<T, TResult> dataQuery,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TResult> queryable = dataQuery.Apply(_dbSet);
        return await queryable.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<TResult>> QueryAsync<TResult>(IDataQuery<T, T> dataQuery, IDataQuery<T, TResult> projection,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> filteredQuery = dataQuery.Apply(_dbSet);
        IQueryable<TResult> projectedQuery = projection.Apply(filteredQuery);
        return await projectedQuery.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<TResult>> QueryPaginatedAsync<TResult>(IDataQuery<T, T> dataQuery,
        IDataQuery<T, TResult> projection, CancellationToken cancellationToken = default)
    {
        IQueryable<T> filteredQuery = dataQuery.Apply(_dbSet);
        IQueryable<TResult> projectedQuery = projection.Apply(filteredQuery);
        return await projectedQuery.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<TResult?> FindAsync<TResult>(IDataQuery<T, TResult> dataQuery,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TResult> queryable = dataQuery.Apply(_dbSet);
        return await queryable.SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<TResult?> FindAsync<TResult>(IDataQuery<T, T> filterDataQuery, IDataQuery<T, TResult> projection,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> filteredQuery = filterDataQuery.Apply(_dbSet);
        IQueryable<TResult> projectedQuery = projection.Apply(filteredQuery);
        return await projectedQuery.SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Remove(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        await SaveAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _dbSet.CountAsync(cancellationToken);
    }
}
