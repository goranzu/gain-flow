using GainFlow.Api.Shared.Common;
using GainFlow.Api.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.Exercises;

public sealed class GetExercisesEndpoint : IEndpoint
{
    public void AddPoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/exercises", async (ApplicationDbContext applicationDbContext,
            CancellationToken cancellationToken,
            GetExercisesQuery query) =>
        {
            IQueryable<GetExerciseResponse> exercisesQuery = applicationDbContext.Exercises
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .Include(ex => ex.MuscleGroups)
                .Select(ex => new GetExerciseResponse
                {
                    Id = ex.Id,
                    Name = ex.Name,
                    PrimaryMuscles = ex.PrimaryMuscles.ToArray(),
                    SecondaryMuscles = ex.SecondaryMuscles.ToArray()
                });

            var response = await PaginatedResponse<GetExerciseResponse>.Create(exercisesQuery,
                query.Page,
                query.PageSize,
                cancellationToken);

            return Results.Ok(response);
        });
    }

    public sealed class GetExercisesQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public sealed class GetExerciseResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> PrimaryMuscles { get; set; }
        public IEnumerable<string> SecondaryMuscles { get; set; }
    }
}
