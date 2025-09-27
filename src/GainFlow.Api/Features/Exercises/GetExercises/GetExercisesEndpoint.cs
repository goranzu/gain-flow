using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence.Queries;
using GainFlow.Api.Shared.Persistence.Queries.Filters;
using GainFlow.Api.Shared.Persistence.Queries.Modifiers;
using GainFlow.Api.Shared.Persistence.Queries.Ordering;
using GainFlow.Api.Shared.Persistence.Queries.Projections;
using Microsoft.AspNetCore.Mvc;

namespace GainFlow.Api.Features.Exercises.GetExercises;

public sealed class GetExercisesEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/exercises", async (
                CancellationToken cancellationToken,
                [FromQuery(Name = "q")] string? search,
                IRepository<Exercise> exerciseRepository,
                int page = 1,
                int pageSize = 10) =>
            {
                DataQuery<Exercise> query = new DataQuery<Exercise>()
                    .Add(new WithoutTrackingModifier<Exercise>())
                    .Add(new ExerciseByNameFilter(search))
                    .Add(new OrderByNameQuery())
                    .Add(new PaginationModifier<Exercise>(page, pageSize));

                var projection = new ExerciseResponseProjection();

                int totalCount = await exerciseRepository.CountAsync(cancellationToken);
                List<ExerciseResponse> exercises =
                    await exerciseRepository.QueryPaginatedAsync(query, projection, cancellationToken);

                var response = new PaginatedResponse<ExerciseResponse>(exercises, totalCount, page, pageSize);

                return Results.Ok(response);
            })
            .RequireAuthorization();
    }
}
