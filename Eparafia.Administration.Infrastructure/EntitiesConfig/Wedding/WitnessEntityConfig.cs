using Eparafia.Administration.Domain.Entities.WeddingEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig.Wedding;

internal sealed class WitnessEntityConfig : IEntityTypeConfiguration<Witness>
{
    public void Configure(EntityTypeBuilder<Witness> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(c => c.WeddingRegister)
            .WithMany(c => c.Witnesses)
            .HasForeignKey(c => c.WeddingRegisterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}