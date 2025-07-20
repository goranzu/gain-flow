using GainFlow.Api.Infrastructure.Data.Enums;

namespace GainFlow.Api.Infrastructure.Data.Entities;

public sealed class Exercise : AuditableEntity
{
    public string Id { get; set; }
    public string Name { get; set; }

    public ICollection<ExerciseMuscleGroup> MuscleGroups { get; set; } = new List<ExerciseMuscleGroup>();

    public IEnumerable<string> PrimaryMuscles =>
        MuscleGroups.Where(mg => mg.Role == MuscleRole.Primary).Select(mg => mg.MuscleGroup.ToString());
    public IEnumerable<string> SecondaryMuscles =>
        MuscleGroups.Where(mg => mg.Role == MuscleRole.Secondary).Select(mg => mg.MuscleGroup.ToString());
}
