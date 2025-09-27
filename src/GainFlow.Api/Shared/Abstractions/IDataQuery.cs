namespace GainFlow.Api.Shared.Abstractions;

public interface IDataQuery<in TEntity, out TResult>
{
    IQueryable<TResult> Apply(IQueryable<TEntity> query);
}
