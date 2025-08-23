using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence;
using GainFlow.Api.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.WorkoutPrograms.ApplyTemplate;

public sealed class ApplyTemplateEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/api/workout-programs/{programId}/apply-template", async (
                string programId,
                CancellationToken cancellationToken,
                ApplicationDbContext context,
                ICurrentUserService currentUserService) =>
            {
                string currentUserId = currentUserService.UserId ?? throw new UnauthorizedAccessException();

                // Get program with template week
                WorkoutProgram? workoutProgram = await context.WorkoutPrograms
                    .Include(wp => wp.Weeks.Where(w => w.WeekNumber == 1))
                    .ThenInclude(w => w.Days)
                    .ThenInclude(d => d.SetGroups)
                    .ThenInclude(sg => sg.Exercises)
                    .FirstOrDefaultAsync(wp => wp.Id == programId && wp.CreatedByUserId == currentUserId, 
                                        cancellationToken);

                if (workoutProgram == null)
                {
                    return Results.NotFound("Workout program not found or access denied");
                }

                ProgramWeek? templateWeek = workoutProgram.Weeks.FirstOrDefault();
                if (templateWeek == null)
                {
                    return Results.BadRequest("No template configured. Please configure template first.");
                }

                // Remove any existing weeks (except template week 1)
                List<ProgramWeek> existingWeeks = await context.ProgramWeeks
                    .Where(w => w.WorkoutProgramId == programId && w.WeekNumber > 1)
                    .ToListAsync(cancellationToken);
                
                context.ProgramWeeks.RemoveRange(existingWeeks);

                // Apply template to all weeks
                var newWeeks = new List<ProgramWeek>();
                for (int weekNumber = 2; weekNumber <= workoutProgram.DurationWeeks; weekNumber++)
                {
                    ProgramWeek newWeek = CloneWeek(templateWeek, weekNumber);
                    newWeeks.Add(newWeek);
                }

                await context.ProgramWeeks.AddRangeAsync(newWeeks, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Results.Ok(new 
                { 
                    message = "Template applied successfully",
                    weeksCreated = workoutProgram.DurationWeeks - 1
                });
            })
            .RequireAuthorization();
    }

    private static ProgramWeek CloneWeek(ProgramWeek templateWeek, int weekNumber)
    {
        string newWeekId = $"pw_{Guid.CreateVersion7()}";
        
        return new ProgramWeek
        {
            Id = newWeekId,
            WorkoutProgramId = templateWeek.WorkoutProgramId,
            WeekNumber = weekNumber,
            Name = $"Week {weekNumber}",
            Description = templateWeek.Description,
            Days = templateWeek.Days.Select(day => CloneDay(day, newWeekId)).ToList()
        };
    }

    private static ProgramDay CloneDay(ProgramDay templateDay, string newWeekId)
    {
        string newDayId = $"pd_{Guid.CreateVersion7()}";
        
        return new ProgramDay
        {
            Id = newDayId,
            ProgramWeekId = newWeekId,
            DayNumber = templateDay.DayNumber,
            Name = templateDay.Name,
            Description = templateDay.Description,
            SetGroups = templateDay.SetGroups.Select(setGroup => CloneSetGroup(setGroup, newDayId)).ToList()
        };
    }

    private static ProgramSetGroup CloneSetGroup(ProgramSetGroup templateSetGroup, string newDayId)
    {
        string newSetGroupId = $"psg_{Guid.CreateVersion7()}";
        
        return new ProgramSetGroup
        {
            Id = newSetGroupId,
            ProgramDayId = newDayId,
            OrderIndex = templateSetGroup.OrderIndex,
            Type = templateSetGroup.Type,
            RestSeconds = templateSetGroup.RestSeconds,
            Notes = templateSetGroup.Notes,
            Exercises = templateSetGroup.Exercises.Select(exercise => CloneExercise(exercise, newSetGroupId)).ToList()
        };
    }

    private static ProgramExercise CloneExercise(ProgramExercise templateExercise, string newSetGroupId)
    {
        return new ProgramExercise
        {
            Id = $"pe_{Guid.CreateVersion7()}",
            ProgramSetGroupId = newSetGroupId,
            ExerciseId = templateExercise.ExerciseId,
            OrderIndex = templateExercise.OrderIndex,
            TargetSets = templateExercise.TargetSets,
            TargetReps = templateExercise.TargetReps,
            TargetWeight = templateExercise.TargetWeight,
            TargetRpe = templateExercise.TargetRpe,
            Notes = templateExercise.Notes,
            ProgressionRule = templateExercise.ProgressionRule
        };
    }
}
