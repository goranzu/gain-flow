namespace GainFlow.Api.Shared.Abstractions;

public interface ICollectionResponse<T>
{
    List<T> Items { get; init; }
}
