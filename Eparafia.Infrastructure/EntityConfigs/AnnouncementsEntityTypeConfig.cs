using Eparafia.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Infrastructure.EntityConfigs;

internal sealed class AnnouncementsEntityTypeConfig : IEntityTypeConfiguration<Announcement>
{
    public void Configure(EntityTypeBuilder<Announcement> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.HasMany(c => c.AnnouncementsRecords)
            .WithOne(c => c.Announcement)
            .HasForeignKey(c => c.AnnouncementId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Parish)
            .WithMany(c => c.Announcements)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(c => c.Author)
            .WithMany(c => c.CreatedAnnouncements)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(c => c.IsActive);
    }
}