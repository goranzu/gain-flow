using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GainFlow.Api.Shared.Persistence.Configurations;

public sealed class ProgramSetGroupConfiguration : IEntityTypeConfiguration<ProgramSetGroup>
{
    public void Configure(EntityTypeBuilder<ProgramSetGroup> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.OrderIndex)
            .IsRequired();

        builder.Property(e => e.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(e => e.RestSeconds)
            .IsRequired();

        builder.Property(e => e.Notes)
            .HasMaxLength(1000);

        builder.HasOne(e => e.ProgramDay)
            .WithMany(pd => pd.SetGroups)
            .HasForeignKey(e => e.ProgramDayId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Exercises)
            .WithOne(pe => pe.ProgramSetGroup)
            .HasForeignKey(pe => pe.ProgramSetGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.ProgramDayId, e.OrderIndex })
            .IsUnique();
    }
}