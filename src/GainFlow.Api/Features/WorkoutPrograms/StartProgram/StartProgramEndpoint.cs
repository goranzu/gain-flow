using FluentValidation;
using FluentValidation.Results;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Extensions;
using GainFlow.Api.Shared.Persistence;
using GainFlow.Api.Shared.Persistence.Enums;
using GainFlow.Api.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.WorkoutPrograms.StartProgram;

public sealed class StartProgramEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/api/workout-programs/{programId}/start", async (
                string programId,
                StartProgramCommand command,
                CancellationToken cancellationToken,
                ApplicationDbContext context,
                IValidator<StartProgramCommand> validator,
                ICurrentUserService currentUserService) =>
            {
                ValidationResult? validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors.ToProblemDetailErrors();
                    return Results.ValidationProblem(errors);
                }

                if (command.WorkoutProgramId != programId)
                {
                    return Results.BadRequest("Program ID in URL must match command");
                }

                string currentUserId = currentUserService.UserId ?? throw new UnauthorizedAccessException();

                // Check if program exists and user has access
                WorkoutProgram? workoutProgram = await context.WorkoutPrograms
                    .FirstOrDefaultAsync(wp => wp.Id == programId && 
                                              (wp.IsPublic || wp.CreatedByUserId == currentUserId), 
                                        cancellationToken);

                if (workoutProgram == null)
                {
                    return Results.NotFound("Workout program not found");
                }

                // Check if user already has an active program
                UserProgram? existingActiveProgram = await context.UserPrograms
                    .FirstOrDefaultAsync(up => up.UserId == currentUserId && 
                                              up.Status == UserProgramStatus.Active, 
                                        cancellationToken);

                if (existingActiveProgram != null)
                {
                    return Results.BadRequest("You already have an active program. Complete or cancel it first.");
                }

                string userProgramId = $"up_{Guid.CreateVersion7()}";

                var userProgram = new UserProgram
                {
                    Id = userProgramId,
                    UserId = currentUserId,
                    WorkoutProgramId = programId,
                    StartDate = command.StartDate,
                    Status = UserProgramStatus.Active,
                    CurrentWeek = 1,
                    CurrentDay = 1,
                    Notes = command.Notes?.Trim()
                };

                await context.UserPrograms.AddAsync(userProgram, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Results.Created($"/api/user-programs/{userProgramId}", new { userProgramId });
            })
            .RequireAuthorization();
    }
}
