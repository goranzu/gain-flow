using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.Exercises;

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
            IQueryable<GetExerciseResponse> exercisesQuery = applicationDbContext.Exercises
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .Include(ex => ex.MuscleGroups)
                .Where(ex => search == null || ex.Name.ToLower().Contains(search.ToLower()))
                .Select(ex => new GetExerciseResponse
                {
                    Id = ex.Id,
                    Name = ex.Name,
                    PrimaryMuscles = ex.PrimaryMuscles.ToArray(),
                    SecondaryMuscles = ex.SecondaryMuscles.ToArray()
                });

            var response = await PaginatedResponse<GetExerciseResponse>.Create(exercisesQuery,
                page,
                pageSize,
                cancellationToken);

            return Results.Ok(response);
        });
    }

    public sealed class GetExerciseResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> PrimaryMuscles { get; set; }
        public IEnumerable<string> SecondaryMuscles { get; set; }
    }
}
