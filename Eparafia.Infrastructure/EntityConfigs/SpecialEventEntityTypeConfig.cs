using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Infrastructure.EntityConfigs;

internal sealed class SpecialEventEntityTypeConfig : IEntityTypeConfiguration<SpecialEvent>
{
    public void Configure(EntityTypeBuilder<SpecialEvent> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Parish)
            .WithMany(c => c.SpecialEvents)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}