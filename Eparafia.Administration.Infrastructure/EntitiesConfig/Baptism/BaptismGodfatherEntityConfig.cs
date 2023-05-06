using Eparafia.Administration.Domain.Entities.BaptismEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig.Baptism;

internal sealed class BaptismGodfatherEntityConfig : IEntityTypeConfiguration<BaptismGodfather>
{
    public void Configure(EntityTypeBuilder<BaptismGodfather> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(c => c.BaptismRegister)
            .WithOne(c => c.Godfather)
            .HasForeignKey<BaptismGodfather>(c => c.BaptismRegisterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}