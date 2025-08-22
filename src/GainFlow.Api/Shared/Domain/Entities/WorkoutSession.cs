namespace GainFlow.Api.Shared.Domain.Entities;

public sealed class WorkoutSession : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string UserProgramId { get; set; } = string.Empty;
    public string ProgramDayId { get; set; } = string.Empty;
    public DateOnly WorkoutDate { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
    public int WeekNumber { get; set; }
    public int DayNumber { get; set; }
    public string? Notes { get; set; }
    public bool IsCompleted { get; set; }

    // Navigation properties
    public UserProgram UserProgram { get; set; } = null!;
    public ProgramDay ProgramDay { get; set; } = null!;
    public ICollection<WorkoutSet> WorkoutSets { get; set; } = [];
}