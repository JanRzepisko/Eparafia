using Eparafia.Administration.Domain.Entities.BaptismEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig.Baptism;

internal sealed class BaptismParentRelationEntityConfig : IEntityTypeConfiguration<BaptismParentsRelation>
{
    public void Configure(EntityTypeBuilder<BaptismParentsRelation> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.HasOne(c => c.Father)
            .WithOne(c => c.BaptismParentsRelation)
            .HasForeignKey<BaptismFather>(c => c.BaptismParentsRelationId)
            .OnDelete(DeleteBehavior.Cascade);        
        
        builder.HasOne(c => c.Mother)
            .WithOne(c => c.BaptismParentsRelation)
            .HasForeignKey<BaptismParentsRelation>(c => c.MotherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}