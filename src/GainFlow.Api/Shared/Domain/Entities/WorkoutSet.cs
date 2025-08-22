namespace GainFlow.Api.Shared.Domain.Entities;

public sealed class WorkoutSet : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string WorkoutSessionId { get; set; } = string.Empty;
    public string ProgramExerciseId { get; set; } = string.Empty;
    public string ExerciseId { get; set; } = string.Empty;
    public int SetNumber { get; set; }
    public int Reps { get; set; }
    public decimal Weight { get; set; }
    public int? Rpe { get; set; } // Rate of Perceived Exertion (1-10)
    public int? RestSeconds { get; set; }
    public string? Notes { get; set; }
    public bool IsWarmup { get; set; }

    // Navigation properties
    public WorkoutSession WorkoutSession { get; set; } = null!;
    public ProgramExercise ProgramExercise { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
}