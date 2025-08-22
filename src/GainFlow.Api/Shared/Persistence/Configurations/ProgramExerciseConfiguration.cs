using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GainFlow.Api.Shared.Persistence.Configurations;

public sealed class ProgramExerciseConfiguration : IEntityTypeConfiguration<ProgramExercise>
{
    public void Configure(EntityTypeBuilder<ProgramExercise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.OrderIndex)
            .IsRequired();

        builder.Property(e => e.TargetSets)
            .IsRequired();

        builder.Property(e => e.TargetReps)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.TargetWeight)
            .HasPrecision(10, 2);

        builder.Property(e => e.TargetRpe)
            .HasMaxLength(20);

        builder.Property(e => e.Notes)
            .HasMaxLength(1000);

        builder.Property(e => e.ProgressionRule)
            .HasMaxLength(500);

        builder.HasOne(e => e.ProgramSetGroup)
            .WithMany(psg => psg.Exercises)
            .HasForeignKey(e => e.ProgramSetGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Exercise)
            .WithMany()
            .HasForeignKey(e => e.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new { e.ProgramSetGroupId, e.OrderIndex })
            .IsUnique();

        builder.HasIndex(e => e.ExerciseId);
    }
}