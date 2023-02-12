using Eparafia.Bible.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Bible.Infrastructure.EntityConfig;

internal sealed class DayEntityConfig : IEntityTypeConfiguration<Day>
{
    public void Configure(EntityTypeBuilder<Day> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.HasMany(c => c.Readings)
            .WithOne(c => c.Day)
            .HasForeignKey(c => c.DayId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}