using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence.Enums;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Persistence.Seeders;

public static class ExerciseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        if (await dbContext.Exercises.AnyAsync())
        {
            return;
        }

        List<Exercise> exercises = GetSeededExercises();
        await dbContext.Exercises.AddRangeAsync(exercises);
        await dbContext.SaveChangesAsync();
    }

    private static List<Exercise> GetSeededExercises()
    {
        return
        [
            CreateExercise("Bench Press", [MuscleGroup.Chest], [MuscleGroup.Triceps, MuscleGroup.Shoulders]),
            CreateExercise("Push-ups", [MuscleGroup.Chest], [MuscleGroup.Triceps, MuscleGroup.Shoulders]),
            CreateExercise("Incline Bench Press", [MuscleGroup.Chest], [MuscleGroup.Triceps, MuscleGroup.Shoulders]),
            CreateExercise("Decline Bench Press", [MuscleGroup.Chest], [MuscleGroup.Triceps, MuscleGroup.Shoulders]),
            CreateExercise("Dumbbell Flyes", [MuscleGroup.Chest], [MuscleGroup.Shoulders]),
            CreateExercise("Chest Dips", [MuscleGroup.Chest], [MuscleGroup.Shoulders, MuscleGroup.Triceps]),
            CreateExercise("Cable Crossover", [MuscleGroup.Chest], [MuscleGroup.Shoulders]),
            CreateExercise("Pec Deck Machine", [MuscleGroup.Chest], [MuscleGroup.Shoulders]),
// Bicep Exercises
            CreateExercise("Bicep Curls", [MuscleGroup.Biceps], [MuscleGroup.Forearms]),
            CreateExercise("Cable Bicep Curls", [MuscleGroup.Biceps], [MuscleGroup.Forearms]),
            CreateExercise("Concentration Curls", [MuscleGroup.Biceps], [MuscleGroup.Forearms]),
            CreateExercise("21s Bicep Curls", [MuscleGroup.Biceps], [MuscleGroup.Forearms]),
// Tricep Exercises
            CreateExercise("Tricep Dips", [MuscleGroup.Triceps], [MuscleGroup.Chest, MuscleGroup.Shoulders]),
            CreateExercise("Overhead Tricep Extension", [MuscleGroup.Triceps], []),
            CreateExercise("Tricep Pushdowns", [MuscleGroup.Triceps], [MuscleGroup.Forearms]),
            CreateExercise("Close-Grip Bench Press", [MuscleGroup.Triceps], [MuscleGroup.Chest, MuscleGroup.Shoulders]),
            CreateExercise("Diamond Push-ups", [MuscleGroup.Triceps], [MuscleGroup.Chest, MuscleGroup.Shoulders]),
            CreateExercise("Skull Crushers", [MuscleGroup.Triceps], [MuscleGroup.Forearms, MuscleGroup.Shoulders]),
// Leg Exercises
            CreateExercise("Squat", [MuscleGroup.Quadriceps, MuscleGroup.Glutes], [MuscleGroup.Hamstrings]),
            CreateExercise("Deadlift", [MuscleGroup.Hamstrings, MuscleGroup.Glutes],
                [MuscleGroup.LowerBack, MuscleGroup.Quadriceps]),
            CreateExercise("Lunges", [MuscleGroup.Quadriceps, MuscleGroup.Glutes],
                [MuscleGroup.Hamstrings, MuscleGroup.Calves]),
            CreateExercise("Leg Press", [MuscleGroup.Quadriceps, MuscleGroup.Glutes], [MuscleGroup.Hamstrings]),
            CreateExercise("Romanian Deadlift", [MuscleGroup.Hamstrings, MuscleGroup.Glutes], [MuscleGroup.LowerBack]),
            CreateExercise("Bulgarian Split Squats", [MuscleGroup.Quadriceps, MuscleGroup.Glutes],
                [MuscleGroup.Hamstrings]),
            CreateExercise("Leg Curls", [MuscleGroup.Hamstrings, MuscleGroup.Glutes], []),
            CreateExercise("Leg Extensions", [MuscleGroup.Quadriceps], []),
            CreateExercise("Calf Raises", [MuscleGroup.Calves], []),
            CreateExercise("Front Squats", [MuscleGroup.Quadriceps, MuscleGroup.Glutes], [MuscleGroup.Abs]),
            CreateExercise("Goblet Squats", [MuscleGroup.Quadriceps, MuscleGroup.Glutes], [MuscleGroup.Abs]),
            CreateExercise("Step-ups", [MuscleGroup.Quadriceps, MuscleGroup.Glutes],
                [MuscleGroup.Hamstrings, MuscleGroup.Calves]),
            // Back Exercises
            CreateExercise("Pull-ups",
                [MuscleGroup.Lats],
                [MuscleGroup.Biceps, MuscleGroup.Abs]),
            CreateExercise("Face Pulls",
                [MuscleGroup.UpperBack],
                [MuscleGroup.Shoulders]),
            CreateExercise("Reverse Flyes",
                [MuscleGroup.UpperBack],
                [MuscleGroup.Shoulders]),
            CreateExercise("Chin-ups",
                [MuscleGroup.Lats, MuscleGroup.Biceps],
                [MuscleGroup.UpperBack, MuscleGroup.Shoulders]),
            CreateExercise("Bent-over Rows",
                [MuscleGroup.UpperBack, MuscleGroup.Lats],
                [MuscleGroup.Biceps, MuscleGroup.Shoulders]),
            CreateExercise("T-Bar Rows",
                [MuscleGroup.UpperBack, MuscleGroup.Lats],
                [MuscleGroup.Biceps, MuscleGroup.Shoulders]),
            CreateExercise("Cable Rows",
                [MuscleGroup.UpperBack, MuscleGroup.Lats],
                [MuscleGroup.Biceps, MuscleGroup.Shoulders]),
            CreateExercise("Lat Pulldowns",
                [MuscleGroup.Lats],
                [MuscleGroup.UpperBack, MuscleGroup.Biceps]),
            CreateExercise("Single-arm Dumbbell Rows",
                [MuscleGroup.UpperBack, MuscleGroup.Lats],
                [MuscleGroup.Biceps]),
// Shoulder Exercises
            CreateExercise("Overhead Press",
                [MuscleGroup.Shoulders],
                [MuscleGroup.Triceps]),
            CreateExercise("Lateral Raises",
                [MuscleGroup.Shoulders],
                []),
            CreateExercise("Front Raises",
                [MuscleGroup.Shoulders],
                []),
            CreateExercise("Rear Delt Flyes",
                [MuscleGroup.Shoulders],
                [MuscleGroup.UpperBack]),
            CreateExercise("Arnold Press",
                [MuscleGroup.Shoulders],
                [MuscleGroup.Triceps]),
            CreateExercise("Pike Push-ups",
                [MuscleGroup.Shoulders],
                [MuscleGroup.Triceps]),
            CreateExercise("Handstand Push-ups",
                [MuscleGroup.Shoulders],
                [MuscleGroup.Triceps, MuscleGroup.Abs]),
            CreateExercise("Upright Rows",
                [MuscleGroup.Shoulders],
                [MuscleGroup.UpperBack]),
// Core/Abs Exercises
            CreateExercise("Plank",
                [MuscleGroup.Abs],
                [MuscleGroup.Shoulders]),
            CreateExercise("Crunches",
                [MuscleGroup.Abs],
                []),
            CreateExercise("Russian Twists",
                [MuscleGroup.Abs],
                []),
            CreateExercise("Mountain Climbers",
                [MuscleGroup.Abs],
                [MuscleGroup.Shoulders, MuscleGroup.Quadriceps]),
            CreateExercise("Bicycle Crunches",
                [MuscleGroup.Abs],
                []),
            CreateExercise("Dead Bug",
                [MuscleGroup.Abs],
                []),
            CreateExercise("Leg Raises",
                [MuscleGroup.Abs],
                []),
            CreateExercise("Hanging Knee Raises",
                [MuscleGroup.Abs],
                [MuscleGroup.Lats, MuscleGroup.Shoulders]),
            CreateExercise("Side Plank",
                [MuscleGroup.Abs],
                [MuscleGroup.Shoulders]),
            CreateExercise("Wood Choppers",
                [MuscleGroup.Abs],
                [MuscleGroup.Shoulders]),
// Forearm Exercises
            CreateExercise("Wrist Curls",
                [MuscleGroup.Forearms],
                []),
            CreateExercise("Reverse Wrist Curls",
                [MuscleGroup.Forearms],
                []),
            CreateExercise("Farmer's Walk",
                [MuscleGroup.Forearms],
                [MuscleGroup.Abs, MuscleGroup.Shoulders]),
            CreateExercise("Plate Pinches",
                [MuscleGroup.Forearms],
                []),
// Compound/Full Body Exercises
            CreateExercise("Burpees",
                [MuscleGroup.Abs, MuscleGroup.Chest],
                [MuscleGroup.Quadriceps, MuscleGroup.Shoulders, MuscleGroup.Triceps]),
            CreateExercise("Thrusters",
                [MuscleGroup.Shoulders, MuscleGroup.Quadriceps],
                [MuscleGroup.Abs, MuscleGroup.Triceps, MuscleGroup.Glutes]),
            CreateExercise("Clean and Press",
                [MuscleGroup.Shoulders, MuscleGroup.UpperBack],
                [MuscleGroup.Quadriceps, MuscleGroup.Abs, MuscleGroup.Triceps]),
            CreateExercise("Turkish Get-ups",
                [MuscleGroup.Abs, MuscleGroup.Shoulders],
                [MuscleGroup.Quadriceps, MuscleGroup.Glutes]),
            CreateExercise("Man Makers",
                [MuscleGroup.Abs, MuscleGroup.Shoulders],
                [MuscleGroup.Chest, MuscleGroup.Quadriceps, MuscleGroup.Triceps]),
// Lower Back Exercises
            CreateExercise("Good Mornings",
                [MuscleGroup.LowerBack, MuscleGroup.Hamstrings],
                [MuscleGroup.Glutes]),
            CreateExercise("Hyperextensions",
                [MuscleGroup.LowerBack],
                [MuscleGroup.Glutes, MuscleGroup.Hamstrings]),
            CreateExercise("Superman",
                [MuscleGroup.LowerBack],
                [MuscleGroup.Glutes]),
// Glute-Specific Exercises
            CreateExercise("Hip Thrusts",
                [MuscleGroup.Glutes],
                [MuscleGroup.Hamstrings]),
            CreateExercise("Glute Bridges",
                [MuscleGroup.Glutes],
                [MuscleGroup.Hamstrings]),
            CreateExercise("Clamshells",
                [MuscleGroup.Glutes],
                []),
            CreateExercise("Fire Hydrants",
                [MuscleGroup.Glutes],
                [MuscleGroup.Abs]),
        ];
    }

    private static Exercise CreateExercise(string name, MuscleGroup[] primaryMuscleGroups,
        MuscleGroup[] secondaryMuscleGroups)
    {
        Exercise exercise = new()
        {
            Id = $"e_{Guid.CreateVersion7()}",
            Name = name,
            MuscleGroups = (List<ExerciseMuscleGroup>) [],
            CreatedAt = DateTime.UtcNow,
        };

        foreach (MuscleGroup primaryMuscleGroup in primaryMuscleGroups)
        {
            exercise.MuscleGroups.Add(new ExerciseMuscleGroup()
            {
                Id = $"emg_{Guid.CreateVersion7()}",
                MuscleGroup = primaryMuscleGroup,
                Role = MuscleRole.Primary,
                CreatedAt = DateTime.UtcNow,
            });
        }

        foreach (MuscleGroup secondaryMuscleGroup in secondaryMuscleGroups)
        {
            exercise.MuscleGroups.Add(new ExerciseMuscleGroup()
            {
                Id = $"emg_{Guid.CreateVersion7()}",
                MuscleGroup = secondaryMuscleGroup,
                Role = MuscleRole.Secondary,
                CreatedAt = DateTime.UtcNow,
            });
        }

        return exercise;
    }
}
