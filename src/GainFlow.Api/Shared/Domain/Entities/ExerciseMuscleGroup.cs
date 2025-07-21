using GainFlow.Api.Shared.Persistence.Enums;

namespace GainFlow.Api.Shared.Domain.Entities;

public sealed class ExerciseMuscleGroup : AuditableEntity
{
    public string Id { get; set; } 
    public string ExerciseId { get; set; } 
    public MuscleGroup MuscleGroup { get; set; }
    public MuscleRole Role { get; set; }
    
    public Exercise Exercise { get; set; }
}
