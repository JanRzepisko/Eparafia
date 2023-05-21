using Eparafia.Administration.Domain.Entities.WeddingEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig.Wedding;

internal sealed class MenEntityConfig : IEntityTypeConfiguration<Men>
{
    public void Configure(EntityTypeBuilder<Men> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(c => c.WeddingRegister)
            .WithOne(c => c.Men)
            .HasForeignKey<WeddingRegister>(c => c.MenId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}