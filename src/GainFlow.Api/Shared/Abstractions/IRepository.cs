namespace GainFlow.Api.Shared.Abstractions;

public interface IRepository<T> where T : class
{
    Task<List<TResult>> QueryAsync<TResult>(IQueryObject<T, TResult> query, CancellationToken cancellationToken = default);

    Task<TResult?> FindAsync<TResult>(IQueryObject<T, TResult> query, CancellationToken cancellationToken = default);
    Task<TResult?> FindAsync<TResult>(IQueryObject<T, T> filterQuery,
        IQueryObject<T, TResult> projection,
        CancellationToken cancellationToken = default);

    Task SaveAsync(CancellationToken cancellationToken = default);
}
