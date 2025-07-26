using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence.Enums;

namespace GainFlow.IntegrationTests;

public class ExerciseTests : IDisposable
{
    private readonly Exercise _exercise;

    public ExerciseTests()
    {
        string exerciseId = $"e_{Guid.CreateVersion7()}";
        _exercise = new Exercise
        {
            Id = exerciseId,
            Name = "Test Exercise",
            MuscleGroups = new List<ExerciseMuscleGroup>()
        };
        _exercise.MuscleGroups.Add(new ExerciseMuscleGroup
        {
            ExerciseId = exerciseId,
            MuscleGroup = MuscleGroup.Abs,
            Role = MuscleRole.Primary
        });
    }

    [Fact]
    public void WhenUpdatingExerciseMuscleGroups_ThenMuscleGroupsAreUpdated()
    {
        _exercise.UpdatePrimaryMuscles(["Abs", "Biceps"]);

        Assert.Equal(2, _exercise.MuscleGroups.Count);
        Assert.Equal(2, _exercise.PrimaryMuscles.ToList().Count);
        Assert.Empty(_exercise.SecondaryMuscles.ToList());

        Assert.Collection(_exercise.MuscleGroups,
            item => Assert.Equal(MuscleGroup.Abs, item.MuscleGroup),
            item => Assert.Equal(MuscleGroup.Biceps, item.MuscleGroup));

        Assert.Collection(_exercise.PrimaryMuscles,
            item => Assert.Equal("Abs", item),
            item => Assert.Equal("Biceps", item));
    }

    public void Dispose()
    {
        // release managed resources here
    }
}
