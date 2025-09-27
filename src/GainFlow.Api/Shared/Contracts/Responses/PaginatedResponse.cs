using GainFlow.Api.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Contracts.Responses;

public class PaginatedResponse<T> : ICollectionResponse<T>
{
    public List<T> Items { get; init; }
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
    public int TotalPages { get; init; }
    public bool HasPreviousPage { get; init; }
    public bool HasNextPage { get; init; }

    public PaginatedResponse(List<T> items, int totalCount, int page, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        PageSize = pageSize;
        PageNumber = page;
        HasPreviousPage = page > 1;
        HasNextPage = page < TotalPages;
    }
}
