using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.Exercises.GetExercise;

public class GetExerciseEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/exercises/{exerciseId}", async (
                string exerciseId,
                ApplicationDbContext applicationDbContext,
                CancellationToken cancellationToken) =>
            {
                ExerciseResponse? exercise = await applicationDbContext.Exercises
                    .Select(ExerciseResponse.Projection())
                    .FirstOrDefaultAsync(ex => ex.Id == exerciseId, cancellationToken: cancellationToken);

                if (exercise is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(exercise);
            })
            .RequireAuthorization();
    }
}
