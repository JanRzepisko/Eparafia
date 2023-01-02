using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;

namespace Eparafia.Application.Repository;

public interface IAnnouncementRepository : IBaseRepository<Announcement>
{
    Task<List<Announcement>> GetLatestAnnouncements(Guid parishId, int page, int pageSize, CancellationToken cancellationToken);
}
