using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence.Queries;

namespace GainFlow.Api.Features.Exercises.DeleteExercise;

public sealed class DeleteExercise : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapDelete("/api/exercises/{exerciseId}", async (
                string exerciseId,
                CancellationToken cancellationToken,
                IRepository<Exercise> exerciseRepository) =>
            {
                DataQuery<Exercise> query = new DataQuery<Exercise>()
                    .Add(new ExerciseById(exerciseId));
                Exercise? exercise = await exerciseRepository.FindAsync(query, cancellationToken);

                if (exercise is null)
                {
                    return Results.NotFound();
                }

                await exerciseRepository.Remove(exercise, cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization();
    }
}
