using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Abstractions;

public class PaginatedResponse<T> : ICollectionResponse<T>
{
    public List<T> Items { get; init; } = [];
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
    public int TotalPages { get; init; }
    public bool HasPreviousPage { get; init; }
    public bool HasNextPage { get; init; }

    public static async Task<PaginatedResponse<T>> Create(IQueryable<T> queryable,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        int totalCount = await queryable.CountAsync(cancellationToken);

        List<T> items = await queryable
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PaginatedResponse<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageSize = pageSize,
            PageNumber = page,
            TotalPages = totalPages,
            HasPreviousPage = page > 1,
            HasNextPage = page < totalPages
        };
    }
}
