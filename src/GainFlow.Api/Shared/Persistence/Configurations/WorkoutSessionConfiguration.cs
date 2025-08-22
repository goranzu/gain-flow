using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GainFlow.Api.Shared.Persistence.Configurations;

public sealed class WorkoutSessionConfiguration : IEntityTypeConfiguration<WorkoutSession>
{
    public void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.WorkoutDate)
            .IsRequired();

        builder.Property(e => e.WeekNumber)
            .IsRequired();

        builder.Property(e => e.DayNumber)
            .IsRequired();

        builder.Property(e => e.Notes)
            .HasMaxLength(1000);

        builder.HasOne(e => e.UserProgram)
            .WithMany(up => up.WorkoutSessions)
            .HasForeignKey(e => e.UserProgramId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ProgramDay)
            .WithMany()
            .HasForeignKey(e => e.ProgramDayId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.WorkoutSets)
            .WithOne(ws => ws.WorkoutSession)
            .HasForeignKey(ws => ws.WorkoutSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.UserProgramId, e.WorkoutDate });
        builder.HasIndex(e => new { e.UserProgramId, e.WeekNumber, e.DayNumber });
    }
}