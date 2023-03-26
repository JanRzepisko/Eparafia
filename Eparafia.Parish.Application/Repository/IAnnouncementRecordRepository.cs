using Eparafia.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Application.Repository;

public interface IAnnouncementRecordRepository : IBaseRepository<AnnouncementRecord>
{
    Task<List<AnnouncementRecord>> SearchInAnnouncements(Guid parishId, string query, int page, int pageSize,
        CancellationToken cancellationToken);
}