using Eparafia.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Infrastructure.EntityConfigs;

internal sealed class CommonEventEntityTypeConfig : IEntityTypeConfiguration<CommonEvent>
{
    public void Configure(EntityTypeBuilder<CommonEvent> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Parish)
            .WithMany(c => c.CommonWeek)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}