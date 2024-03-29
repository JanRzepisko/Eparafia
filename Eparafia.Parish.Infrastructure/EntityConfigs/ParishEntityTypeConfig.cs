using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Infrastructure.EntityConfigs;

internal sealed class ParishEntityTypeConfig : IEntityTypeConfiguration<Parish>
{
    public void Configure(EntityTypeBuilder<Parish> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.HasMany(c => c.Users)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(c => c.Priests)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(c => c.Posts)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.SpecialEvents)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.CommonWeek)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Payments)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}