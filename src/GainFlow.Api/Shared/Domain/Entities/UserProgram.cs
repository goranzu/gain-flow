using GainFlow.Api.Shared.Persistence.Enums;

namespace GainFlow.Api.Shared.Domain.Entities;

public sealed class UserProgram : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string WorkoutProgramId { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public UserProgramStatus Status { get; set; }
    public int CurrentWeek { get; set; } = 1;
    public int CurrentDay { get; set; } = 1;
    public string? Notes { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public WorkoutProgram WorkoutProgram { get; set; } = null!;
    public ICollection<WorkoutSession> WorkoutSessions { get; set; } = [];
}