using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Extensions;
using GainFlow.Api.Shared.Persistence;
using GainFlow.Api.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.WorkoutPrograms.GetWorkoutPrograms;

public sealed class GetWorkoutProgramsEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/workout-programs", async (
                ApplicationDbContext context,
                ICurrentUserService currentUserService,
                CancellationToken cancellationToken,
                int page = 1,
                int pageSize = 10,
                [FromQuery(Name = "q")] string? search = null,
                bool? isPublic = null) =>
            {
                string currentUserId = currentUserService.UserId ?? throw new UnauthorizedAccessException();

                IQueryable<WorkoutProgram> query = context.WorkoutPrograms
                    .Include(wp => wp.CreatedByUser)
                    .Where(wp => wp.IsPublic || wp.CreatedByUserId == currentUserId);

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(wp => wp.Name.Contains(search) || wp.Description.Contains(search));
                }

                if (isPublic.HasValue)
                {
                    query = query.Where(wp => wp.IsPublic == isPublic.Value);
                }

                IQueryable<WorkoutProgramResponse> workoutProgramQuery = query
                    .OrderByDescending(wp => wp.CreatedAt)
                    .Select(wp => new WorkoutProgramResponse
                    {
                        Id = wp.Id,
                        Name = wp.Name,
                        Description = wp.Description,
                        DurationWeeks = wp.DurationWeeks,
                        IsPublic = wp.IsPublic,
                        CreatedAt = wp.CreatedAt
                    });

                var response = await PaginatedResponse<WorkoutProgramResponse>.Create(
                    workoutProgramQuery,
                    page,
                    pageSize,
                    cancellationToken);

                return Results.Ok(response);
            })
            .RequireAuthorization();
    }
}
