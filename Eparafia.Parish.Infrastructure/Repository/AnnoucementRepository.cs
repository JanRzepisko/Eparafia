using Eparafia.Application.Repository;
using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Infrastructure.Repository;

public class AnnouncementRepository : BaseRepository<Announcement>, IAnnouncementRepository
{
    public AnnouncementRepository(DbSet<Announcement>? entities) : base(entities)
    {
    }

    public Task<List<Announcement>> GetLatestAnnouncements(Guid parishId, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        return _entities
            .Include(c => c.AnnouncementsRecords)
            .AsQueryable()
            .Where(c => c.ParishId == parishId && c.PublishDate <= DateTime.Now)
            .OrderByDescending(c => c.PublishDate)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
    
    public Task<Announcement> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _entities
            .Include(c => c.AnnouncementsRecords)
            .AsQueryable()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public Task<List<Announcement>> GetAnnouncementsBeforePublish(Guid parishId, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        return _entities
            .Include(c => c.AnnouncementsRecords)
            .AsQueryable()
            .Where(c => c.ParishId == parishId && c.PublishDate > DateTime.Now)
            .OrderByDescending(c => c.PublishDate)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);    
    }
}