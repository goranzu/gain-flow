using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GainFlow.Api.Shared.Persistence.Configurations;

public sealed class ProgramWeekConfiguration : IEntityTypeConfiguration<ProgramWeek>
{
    public void Configure(EntityTypeBuilder<ProgramWeek> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.WeekNumber)
            .IsRequired();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.HasOne(e => e.WorkoutProgram)
            .WithMany(wp => wp.Weeks)
            .HasForeignKey(e => e.WorkoutProgramId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Days)
            .WithOne(d => d.ProgramWeek)
            .HasForeignKey(d => d.ProgramWeekId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.WorkoutProgramId, e.WeekNumber })
            .IsUnique();
    }
}