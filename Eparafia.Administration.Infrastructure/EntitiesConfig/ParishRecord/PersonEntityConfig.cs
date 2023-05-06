using Eparafia.Administration.Domain.Entities.ParishRecord;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig.ParishRecord;

internal sealed class PersonEntityConfig : IEntityTypeConfiguration<HomeRecord>
{
    public void Configure(EntityTypeBuilder<HomeRecord> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.OwnsOne(c => c.Address);

        builder.HasOne(c => c.Parish)
            .WithMany(c => c.HomeRecords)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Persons)
            .WithOne(c => c.HomeRecord)
            .HasForeignKey(c => c.HomeRecordId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}