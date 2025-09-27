namespace GainFlow.Api.Shared.Abstractions;

public interface IRepository<T> where T : class
{
    Task<List<TResult>> QueryAsync<TResult>(IDataQuery<T, TResult> dataQuery,
        CancellationToken cancellationToken = default);

    Task<List<TResult>> QueryAsync<TResult>(IDataQuery<T, T> dataQuery, IDataQuery<T, TResult> projection,
        CancellationToken cancellationToken = default);

    Task<List<TResult>> QueryPaginatedAsync<TResult>(IDataQuery<T, T> dataQuery, IDataQuery<T, TResult> projection,
        CancellationToken cancellationToken = default);

    Task<TResult?> FindAsync<TResult>(IDataQuery<T, TResult> dataQuery, CancellationToken cancellationToken = default);

    Task<TResult?> FindAsync<TResult>(IDataQuery<T, T> filterDataQuery,
        IDataQuery<T, TResult> projection,
        CancellationToken cancellationToken = default);

    Task SaveAsync(CancellationToken cancellationToken = default);
    Task Remove(T entity, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
}
