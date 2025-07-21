using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GainFlow.Api.Shared.Persistence.Configurations;

public sealed class ExerciseMuscleGroupConfiguration : IEntityTypeConfiguration<ExerciseMuscleGroup>
{
    public void Configure(EntityTypeBuilder<ExerciseMuscleGroup> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.MuscleGroup).HasConversion<string>();
        builder.Property(x => x.Role).HasConversion<string>();

        builder.HasIndex(x => new { x.ExerciseId, x.MuscleGroup, x.Role })
            .IsUnique();
    }
}
