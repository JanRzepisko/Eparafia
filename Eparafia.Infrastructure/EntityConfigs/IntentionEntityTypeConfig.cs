using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Infrastructure.EntityConfigs;

internal sealed class IntentionEntityTypeConfig : IEntityTypeConfiguration<Intention>
{
    public void Configure(EntityTypeBuilder<Intention> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Parish)
            .WithMany(c => c.Intentions)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}