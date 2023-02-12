using Eparafia.Bible.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Bible.Infrastructure.EntityConfig;

internal sealed class ReadingEntityConfig : IEntityTypeConfiguration<Reading>
{
    public void Configure(EntityTypeBuilder<Reading> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.HasOne(c => c.Day)
            .WithMany(c => c.Readings)
            .HasForeignKey(c => c.DayId);
    }
}