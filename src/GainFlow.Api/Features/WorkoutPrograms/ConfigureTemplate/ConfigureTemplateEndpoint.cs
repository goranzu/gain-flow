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

namespace GainFlow.Api.Features.WorkoutPrograms.ConfigureTemplate;

public sealed class ConfigureTemplateEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/api/workout-programs/{programId}/configure-template", async (
                string programId,
                ConfigureTemplateCommand command,
                CancellationToken cancellationToken,
                ApplicationDbContext context,
                IValidator<ConfigureTemplateCommand> validator,
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

                // Check if program exists and user owns it
                WorkoutProgram? workoutProgram = await context.WorkoutPrograms
                    .Include(wp => wp.Weeks)
                    .ThenInclude(w => w.Days)
                    .ThenInclude(d => d.SetGroups)
                    .ThenInclude(sg => sg.Exercises)
                    .FirstOrDefaultAsync(wp => wp.Id == programId && wp.CreatedByUserId == currentUserId, 
                                        cancellationToken);

                if (workoutProgram == null)
                {
                    return Results.NotFound("Workout program not found or access denied");
                }

                // Clear existing template (week 1 serves as template)
                ProgramWeek? existingTemplate = workoutProgram.Weeks.FirstOrDefault(w => w.WeekNumber == 1);
                if (existingTemplate != null)
                {
                    context.ProgramWeeks.Remove(existingTemplate);
                }

                // Create new template week
                ProgramWeek templateWeek = CreateTemplateWeek(command, programId);
                await context.ProgramWeeks.AddAsync(templateWeek, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Results.Ok(new { message = "Template configured successfully" });
            })
            .RequireAuthorization();
    }

    private static ProgramWeek CreateTemplateWeek(ConfigureTemplateCommand command, string programId)
    {
        string weekId = $"pw_{Guid.CreateVersion7()}";
        return new ProgramWeek
        {
            Id = weekId,
            WorkoutProgramId = programId,
            WeekNumber = 1, // Week 1 serves as the template
            Name = "Template Week",
            Description = "Base template for all weeks",
            Days = CreateTemplateDays(command.Days, weekId)
        };
    }

    private static List<ProgramDay> CreateTemplateDays(List<TemplateDayRequest> dayRequests, string weekId)
    {
        return dayRequests.Select(day =>
        {
            string dayId = $"pd_{Guid.CreateVersion7()}";
            return new ProgramDay
            {
                Id = dayId,
                ProgramWeekId = weekId,
                DayNumber = day.DayNumber,
                Name = day.Name.Trim(),
                Description = day.Description?.Trim(),
                SetGroups = CreateTemplateSetGroups(day.SetGroups, dayId)
            };
        }).ToList();
    }

    private static List<ProgramSetGroup> CreateTemplateSetGroups(List<TemplateSetGroupRequest> setGroupRequests, string dayId)
    {
        return setGroupRequests.Select(setGroup =>
        {
            string setGroupId = $"psg_{Guid.CreateVersion7()}";
            return new ProgramSetGroup
            {
                Id = setGroupId,
                ProgramDayId = dayId,
                OrderIndex = setGroup.OrderIndex,
                Type = Enum.Parse<SetGroupType>(setGroup.Type),
                RestSeconds = setGroup.RestSeconds,
                Notes = setGroup.Notes?.Trim(),
                Exercises = CreateTemplateExercises(setGroup.Exercises, setGroupId)
            };
        }).ToList();
    }

    private static List<ProgramExercise> CreateTemplateExercises(List<TemplateExerciseRequest> exerciseRequests, string setGroupId)
    {
        return exerciseRequests.Select(exercise => new ProgramExercise
        {
            Id = $"pe_{Guid.CreateVersion7()}",
            ProgramSetGroupId = setGroupId,
            ExerciseId = exercise.ExerciseId,
            OrderIndex = exercise.OrderIndex,
            TargetSets = exercise.TargetSets,
            TargetReps = exercise.TargetReps.Trim(),
            TargetWeight = exercise.TargetWeight,
            TargetRpe = exercise.TargetRpe?.Trim(),
            Notes = exercise.Notes?.Trim(),
            ProgressionRule = exercise.ProgressionRule?.Trim()
        }).ToList();
    }
}
