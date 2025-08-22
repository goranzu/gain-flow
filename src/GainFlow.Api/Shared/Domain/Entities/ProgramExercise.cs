namespace GainFlow.Api.Shared.Domain.Entities;

public sealed class ProgramExercise : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string ProgramSetGroupId { get; set; } = string.Empty;
    public string ExerciseId { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    public int TargetSets { get; set; }
    public string TargetReps { get; set; } = string.Empty; // e.g., "8-12", "AMRAP", "3x5"
    public decimal? TargetWeight { get; set; }
    public string? TargetRpe { get; set; } // e.g., "7-8", "9"
    public string? Notes { get; set; }
    public string? ProgressionRule { get; set; } // JSON or simple rule like "+5lbs when all sets hit max reps"

    // Navigation properties
    public ProgramSetGroup ProgramSetGroup { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
}