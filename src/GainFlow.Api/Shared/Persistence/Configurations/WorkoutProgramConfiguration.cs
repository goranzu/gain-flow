using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GainFlow.Api.Shared.Persistence.Configurations;

public sealed class WorkoutProgramConfiguration : IEntityTypeConfiguration<WorkoutProgram>
{
    public void Configure(EntityTypeBuilder<WorkoutProgram> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.CreatedByUserId)
            .IsRequired();

        builder.Property(e => e.DurationWeeks)
            .IsRequired();

        builder.HasOne(e => e.CreatedByUser)
            .WithMany()
            .HasForeignKey(e => e.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Weeks)
            .WithOne(w => w.WorkoutProgram)
            .HasForeignKey(w => w.WorkoutProgramId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.UserPrograms)
            .WithOne(up => up.WorkoutProgram)
            .HasForeignKey(up => up.WorkoutProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.CreatedByUserId);
        builder.HasIndex(e => e.IsPublic);
    }
}