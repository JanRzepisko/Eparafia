using Eparafia.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Infrastructure.EntityConfigs;

internal sealed class AnnouncementsRecordsEntityTypeConfig : IEntityTypeConfiguration<AnnouncementsRecords>
{
    public void Configure(EntityTypeBuilder<AnnouncementsRecords> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.HasOne(c => c.Announcement)
            .WithMany(c => c.AnnouncementsRecords)
            .HasForeignKey(c => c.AnnouncementId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}