using GainFlow.Api.Shared.Abstractions;

namespace GainFlow.Api.Features.Exercises;

public sealed class GetExercisesEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/exercises", async (
            CancellationToken cancellationToken,
            IQueryHandler<GetExercisesQuery, PaginatedResponse<GetExerciseResponse>> handler,
            int page = 1,
            int pageSize = 10) =>
        {
            var query = new GetExercisesQuery
            {
                Page = page,
                PageSize = pageSize
            };

            PaginatedResponse<GetExerciseResponse> result = await handler.Handle(query, cancellationToken);

            return Results.Ok(result);
        });
    }
}
