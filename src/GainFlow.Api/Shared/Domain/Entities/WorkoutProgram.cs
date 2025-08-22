namespace GainFlow.Api.Shared.Domain.Entities;

public sealed class WorkoutProgram : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CreatedByUserId { get; set; } = string.Empty;
    public int DurationWeeks { get; set; }
    public bool IsPublic { get; set; }

    // Navigation properties
    public User CreatedByUser { get; set; } = null!;
    public ICollection<ProgramWeek> Weeks { get; set; } = [];
    public ICollection<UserProgram> UserPrograms { get; set; } = [];
}