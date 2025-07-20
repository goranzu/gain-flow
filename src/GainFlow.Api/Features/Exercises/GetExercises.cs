using GainFlow.Api.Infrastructure.Data;
using GainFlow.Api.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.Exercises;

public sealed class GetExercises : IEndpoint
{
    public void AddPoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/exercises", async (ApplicationDbContext applicationDbContext,
            CancellationToken cancellationToken,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10) =>
        {
            IQueryable<ExerciseResponse> exercisesQuery = applicationDbContext.Exercises
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .Include(ex => ex.MuscleGroups)
                .Select(ex => new ExerciseResponse
                {
                    Id = ex.Id,
                    Name = ex.Name,
                    PrimaryMuscles = ex.PrimaryMuscles.ToArray(),
                    SecondaryMuscles = ex.SecondaryMuscles.ToArray()
                });

            var response = await PaginatedResponse<ExerciseResponse>.Create(exercisesQuery,
                page,
                pageSize,
                cancellationToken);

            return Results.Ok(response);
        });
    }
}
