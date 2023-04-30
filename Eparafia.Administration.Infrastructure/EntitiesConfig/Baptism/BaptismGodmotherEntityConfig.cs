using Eparafia.Administration.Domain.Entities;
using Eparafia.Administration.Domain.Entities.BaptismParents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig;

internal sealed class BaptismGodmotherEntityConfig : IEntityTypeConfiguration<BaptismGodmother>
{
    public void Configure(EntityTypeBuilder<BaptismGodmother> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(c => c.BaptismRegister)
            .WithOne(c => c.Godmother)
            .HasForeignKey<BaptismGodmother>(c => c.BaptismRegisterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}