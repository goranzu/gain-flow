using GainFlow.Api.Shared.Persistence.Enums;

namespace GainFlow.Api.Shared.Domain.Entities;

public sealed class ProgramSetGroup : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string ProgramDayId { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    public SetGroupType Type { get; set; }
    public int RestSeconds { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public ProgramDay ProgramDay { get; set; } = null!;
    public ICollection<ProgramExercise> Exercises { get; set; } = [];
}