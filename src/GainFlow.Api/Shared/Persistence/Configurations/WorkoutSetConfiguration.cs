using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GainFlow.Api.Shared.Persistence.Configurations;

public sealed class WorkoutSetConfiguration : IEntityTypeConfiguration<WorkoutSet>
{
    public void Configure(EntityTypeBuilder<WorkoutSet> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.SetNumber)
            .IsRequired();

        builder.Property(e => e.Reps)
            .IsRequired();

        builder.Property(e => e.Weight)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(e => e.Rpe);

        builder.ToTable(t => t.HasCheckConstraint("CK_WorkoutSet_Rpe", "Rpe BETWEEN 1 AND 10"));

        builder.Property(e => e.Notes)
            .HasMaxLength(500);

        builder.HasOne(e => e.WorkoutSession)
            .WithMany(ws => ws.WorkoutSets)
            .HasForeignKey(e => e.WorkoutSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ProgramExercise)
            .WithMany()
            .HasForeignKey(e => e.ProgramExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Exercise)
            .WithMany()
            .HasForeignKey(e => e.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new { e.WorkoutSessionId, e.ProgramExerciseId, e.SetNumber })
            .IsUnique();

        builder.HasIndex(e => new { e.ExerciseId, e.WorkoutSessionId });
    }
}