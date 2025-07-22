namespace GainFlow.Api.Shared.Abstractions;

public interface ICollectionResponse<T>
{
    public List<T> Items { get; init; }
}
