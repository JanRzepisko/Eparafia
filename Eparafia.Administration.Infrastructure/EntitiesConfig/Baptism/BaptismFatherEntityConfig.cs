using Eparafia.Administration.Domain.Entities.BaptismEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig.Baptism;

internal sealed class BaptismFatherEntityConfig : IEntityTypeConfiguration<BaptismFather>
{
    public void Configure(EntityTypeBuilder<BaptismFather> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        //builder.HasOne(c => c.BaptismParentsRelation)
        //    .WithOne(c => c.Father)
        //    .HasForeignKey<BaptismFather>(c => c.BaptismParentsRelationId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}