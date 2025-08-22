using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GainFlow.Api.Shared.Persistence.Configurations;

public sealed class UserProgramConfiguration : IEntityTypeConfiguration<UserProgram>
{
    public void Configure(EntityTypeBuilder<UserProgram> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(e => e.CurrentWeek)
            .IsRequired();

        builder.Property(e => e.CurrentDay)
            .IsRequired();

        builder.Property(e => e.Notes)
            .HasMaxLength(1000);

        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.WorkoutProgram)
            .WithMany(wp => wp.UserPrograms)
            .HasForeignKey(e => e.WorkoutProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.WorkoutSessions)
            .WithOne(ws => ws.UserProgram)
            .HasForeignKey(ws => ws.UserProgramId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.UserId);
        builder.HasIndex(e => new { e.UserId, e.Status });
    }
}