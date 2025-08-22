namespace GainFlow.Api.Shared.Domain.Entities;

public sealed class ProgramDay : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string ProgramWeekId { get; set; } = string.Empty;
    public int DayNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation properties
    public ProgramWeek ProgramWeek { get; set; } = null!;
    public ICollection<ProgramSetGroup> SetGroups { get; set; } = [];
}