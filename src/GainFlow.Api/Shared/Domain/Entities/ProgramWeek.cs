namespace GainFlow.Api.Shared.Domain.Entities;

public sealed class ProgramWeek : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string WorkoutProgramId { get; set; } = string.Empty;
    public int WeekNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation properties
    public WorkoutProgram WorkoutProgram { get; set; } = null!;
    public ICollection<ProgramDay> Days { get; set; } = [];
}