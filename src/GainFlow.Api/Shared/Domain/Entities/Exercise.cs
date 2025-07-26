using GainFlow.Api.Shared.Persistence.Enums;

namespace GainFlow.Api.Shared.Domain.Entities;

public sealed class Exercise : AuditableEntity
{
    public string Id { get; set; }
    public string Name { get; set; }

    public ICollection<ExerciseMuscleGroup> MuscleGroups { get; set; } = new List<ExerciseMuscleGroup>();

    public IEnumerable<string> PrimaryMuscles =>
        MuscleGroups.Where(mg => mg.Role == MuscleRole.Primary).Select(mg => mg.MuscleGroup.ToString());

    public IEnumerable<string> SecondaryMuscles =>
        MuscleGroups.Where(mg => mg.Role == MuscleRole.Secondary).Select(mg => mg.MuscleGroup.ToString());

    public void UpdatePrimaryMuscles(string[]? requestedMuscles)
        => UpdateMuscleGroups(MuscleRole.Primary, requestedMuscles);

    public void UpdateSecondaryMuscles(string[]? requestedMuscles)
        => UpdateMuscleGroups(MuscleRole.Secondary, requestedMuscles);

    private void UpdateMuscleGroups(MuscleRole role, string[]? requestedMuscles)
    {
        if (requestedMuscles is null)
        {
            return;
        }

        var requestedMuscleGroups = requestedMuscles
            .Select(m => Enum.Parse<MuscleGroup>(m, ignoreCase: true))
            .ToHashSet();

        var existingMuscleGroups = MuscleGroups
            .Where(eg => eg.Role == role)
            .Select(eg => eg.MuscleGroup)
            .ToHashSet();

        var toRemove = MuscleGroups
            .Where(mg => mg.Role == role && !requestedMuscleGroups.Contains(mg.MuscleGroup))
            .ToList();

        foreach (ExerciseMuscleGroup mg in toRemove)
        {
            MuscleGroups.Remove(mg);
        }

        ExerciseMuscleGroup[] toAdd = requestedMuscleGroups
            .Where(muscle => !existingMuscleGroups.Contains(muscle))
            .Select(muscle => new ExerciseMuscleGroup
            {
                Id = $"eg_{Guid.CreateVersion7()}",
                ExerciseId = Id,
                MuscleGroup = muscle,
                Role = role
            })
            .ToArray();

        foreach (ExerciseMuscleGroup exerciseMuscleGroup in toAdd)
        {
            MuscleGroups.Add(exerciseMuscleGroup);
        }

        ValidateMuscleGroupState();
    }

    private void ValidateMuscleGroupState()
    {
        if (PrimaryMuscles.Intersect(SecondaryMuscles).Any())
        {
            throw new InvalidOperationException("Primary and secondary muscle groups cannot overlap.");
        }

        if (!PrimaryMuscles.Any())
        {
            throw new InvalidOperationException("At least one primary muscle group is required.");
        }
    }
}
