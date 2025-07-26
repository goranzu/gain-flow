using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.Exercises.DeleteExercise;

public sealed class DeleteExercise : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapDelete("/api/exercises/{exerciseId}", async (
                string exerciseId,
                ApplicationDbContext applicationDbContext,
                CancellationToken cancellationToken) =>
            {
                Exercise? exercise = await applicationDbContext.Exercises
                    .FirstOrDefaultAsync(e => e.Id == exerciseId, cancellationToken: cancellationToken);

                if (exercise is null)
                {
                    return Results.NotFound();
                }

                applicationDbContext.Exercises.Remove(exercise);
                await applicationDbContext.SaveChangesAsync(cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization();
    }
}
