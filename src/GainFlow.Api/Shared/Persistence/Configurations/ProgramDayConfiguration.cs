using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GainFlow.Api.Shared.Persistence.Configurations;

public sealed class ProgramDayConfiguration : IEntityTypeConfiguration<ProgramDay>
{
    public void Configure(EntityTypeBuilder<ProgramDay> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DayNumber)
            .IsRequired();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.HasOne(e => e.ProgramWeek)
            .WithMany(pw => pw.Days)
            .HasForeignKey(e => e.ProgramWeekId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.SetGroups)
            .WithOne(sg => sg.ProgramDay)
            .HasForeignKey(sg => sg.ProgramDayId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.ProgramWeekId, e.DayNumber })
            .IsUnique();
    }
}