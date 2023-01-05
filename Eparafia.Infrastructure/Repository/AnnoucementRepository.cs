using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;
using Eparafia.Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eparafia.Infrastructure.Repository;

public class AnnouncementRepository : BaseRepository<Announcement>,  IAnnouncementRepository
{
    public AnnouncementRepository(DbSet<Announcement>? entities) : base(entities)
    {
    }

    public Task<List<Announcement>> GetLatestAnnouncements(Guid parishId, int page, int pageSize, CancellationToken cancellationToken)
    {
        return _entities
            .Include(c => c.AnnouncementsRecords)
            .AsQueryable()
            .Where(c => c.ParishId == parishId && c.IsActive)
            .OrderByDescending(c => c.Date)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}  