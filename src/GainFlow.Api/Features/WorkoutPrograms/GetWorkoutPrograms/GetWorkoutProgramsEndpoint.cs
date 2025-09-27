using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence.Queries;
using GainFlow.Api.Shared.Persistence.Queries.Filters;
using GainFlow.Api.Shared.Persistence.Queries.Includes;
using GainFlow.Api.Shared.Persistence.Queries.Ordering;
using GainFlow.Api.Shared.Persistence.Queries.Projections;
using GainFlow.Api.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace GainFlow.Api.Features.WorkoutPrograms.GetWorkoutPrograms;

public sealed class GetWorkoutProgramsEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/workout-programs", async (
                IRepository<WorkoutProgram> workoutProgramRepository,
                ICurrentUserService currentUserService,
                CancellationToken cancellationToken,
                int page = 1,
                int pageSize = 10,
                [FromQuery(Name = "q")] string? search = null,
                bool? isPublic = null) =>
            {
                string currentUserId = currentUserService.UserId ?? throw new UnauthorizedAccessException();

                DataQuery<WorkoutProgram> query = new DataQuery<WorkoutProgram>()
                    .WithoutTracking()
                    .Add(new WithCreatedByUserInclude())
                    .Add(new WorkoutProgramByPublicOrUserFilter(currentUserId))
                    .Add(new WorkoutProgramByNameFilter(search));

                if (isPublic.HasValue)
                {
                    query.Add(new WorkoutProgramByPublicFilter(isPublic.Value));
                }

                query.Add(new OrderByCreatedAtDescendingQuery<WorkoutProgram>())
                     .Paginate(page, pageSize);

                var projection = new WorkoutProgramResponseProjection();

                int totalCount = await workoutProgramRepository.CountAsync(cancellationToken);
                List<WorkoutProgramResponse> workoutPrograms =
                    await workoutProgramRepository.QueryPaginatedAsync(query, projection, cancellationToken);

                var response = new PaginatedResponse<WorkoutProgramResponse>(workoutPrograms, totalCount, page, pageSize);

                return Results.Ok(response);
            })
            .RequireAuthorization();
    }
}
