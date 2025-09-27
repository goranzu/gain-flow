using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence.Queries;

namespace GainFlow.Api.Features.Exercises.GetExercise;

public class GetExerciseEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/exercises/{exerciseId}", async (
                string exerciseId,
                CancellationToken cancellationToken,
                IRepository<Exercise> exerciseRepository) =>
            {
                DataQuery<Exercise> dataQuery = new DataQuery<Exercise>()
                    .Add(new ExerciseById(exerciseId));
                var projection = new ExerciseResponseProjection();

                ExerciseResponse? exercise = await exerciseRepository.FindAsync(dataQuery, projection, cancellationToken);

                if (exercise is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(exercise);
            })
            .RequireAuthorization();
    }
}
