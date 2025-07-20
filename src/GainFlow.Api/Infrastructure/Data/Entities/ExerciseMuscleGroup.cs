using GainFlow.Api.Infrastructure.Data.Enums;

namespace GainFlow.Api.Infrastructure.Data.Entities;

public sealed class ExerciseMuscleGroup : AuditableEntity
{
    public string Id { get; set; } 
    public string ExerciseId { get; set; } 
    public MuscleGroup MuscleGroup { get; set; }
    public MuscleRole Role { get; set; }
    
    public Exercise Exercise { get; set; }
}
