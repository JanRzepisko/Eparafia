using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Infrastructure.EntityConfigs;

internal sealed class PriestEntityTypeConfig : IEntityTypeConfiguration<Priest>
{
    public void Configure(EntityTypeBuilder<Priest> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Parish)
            .WithMany(c => c.Priests)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.SetNull);
        
    }
}