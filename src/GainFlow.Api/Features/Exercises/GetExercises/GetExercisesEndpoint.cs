using System.Linq.Expressions;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.Exercises.GetExercises;

public sealed class GetExercisesEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/exercises", async (
                CancellationToken cancellationToken,
                ApplicationDbContext applicationDbContext,
                [FromQuery(Name = "q")] string? search,
                int page = 1,
                int pageSize = 10) =>
            {
                IQueryable<ExerciseResponse> exercisesQuery = applicationDbContext.Exercises
                    .AsNoTracking()
                    .OrderBy(e => e.Name)
                    .Include(ex => ex.MuscleGroups)
                    .Where(ex => search == null || ex.Name.ToLower().Contains(search.ToLower()))
                    .Select(ExerciseResponse.Projection());

                var response = await PaginatedResponse<ExerciseResponse>.Create(exercisesQuery,
                    page,
                    pageSize,
                    cancellationToken);

                return Results.Ok(response);
            })
            .RequireAuthorization();
    }
}
