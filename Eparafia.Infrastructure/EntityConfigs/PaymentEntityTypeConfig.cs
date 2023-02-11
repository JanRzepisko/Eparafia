using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Infrastructure.EntityConfigs;

internal sealed class PaymentEntityTypeConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Parish)
            .WithMany(c => c.Payments)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}