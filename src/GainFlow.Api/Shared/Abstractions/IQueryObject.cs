namespace GainFlow.Api.Shared.Abstractions;

public interface IQueryObject<in TEntity, out TResult>
{
    IQueryable<TResult> Apply(IQueryable<TEntity> query);
}
