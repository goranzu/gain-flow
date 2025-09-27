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

    public async Task<List<TResult>> QueryAsync<TResult>(IQueryObject<T, TResult> query,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TResult> queryable = query.Apply(_dbSet);
        return await queryable.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<TResult?> FindAsync<TResult>(IQueryObject<T, TResult> query,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TResult> queryable = query.Apply(_dbSet);
        return await queryable.SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<TResult?> FindAsync<TResult>(IQueryObject<T, T> filterQuery, IQueryObject<T, TResult> projection,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> filteredQuery = filterQuery.Apply(_dbSet);
        IQueryable<TResult> projectedQuery = projection.Apply(filteredQuery);
        return await projectedQuery.SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
