using Eparafia.Administration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig;

internal sealed class PriestEntityConfig : IEntityTypeConfiguration<Priest>
{
    public void Configure(EntityTypeBuilder<Priest> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        
        builder.HasOne(c => c.Parish)
            .WithMany(c => c.Priests)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}