namespace GainFlow.Api.Shared.Common;

public interface ICollectionResponse<T>
{
    public List<T> Items { get; init; }
}
