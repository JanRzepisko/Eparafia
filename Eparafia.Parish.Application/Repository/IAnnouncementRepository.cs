using Eparafia.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Application.Repository;

public interface IAnnouncementRepository : IBaseRepository<Announcement>
{
    Task<List<Announcement>> GetLatestAnnouncements(Guid parishId, int page, int pageSize,
        CancellationToken cancellationToken);
    
    Task<Announcement> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Announcement>> GetAnnouncementsBeforePublish(Guid parishId, int page, int pageSize, CancellationToken cancellationToken);
}