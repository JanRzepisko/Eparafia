using Eparafia.Application.Repository;
using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Infrastructure.Repository;

public class AnnouncementRecordRepository : BaseRepository<AnnouncementRecord>, IAnnouncementRecordRepository
{
    public AnnouncementRecordRepository(DbSet<AnnouncementRecord>? entities) : base(entities)
    {
    }

    public Task<List<AnnouncementRecord>> SearchInAnnouncements(Guid parishId, string query, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        return _entities
            .Include(c => c.Announcement)
            .AsQueryable()
            .Where(c => c.Announcement.ParishId == parishId && c.Announcement.PublishDate < DateTime.Today &&
                        c.Content.ToLower().Contains(query.ToLower()))
            .OrderByDescending(c => c.Announcement.PublishDate)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}