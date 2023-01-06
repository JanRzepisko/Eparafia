using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;

namespace Eparafia.Application.Repository;

public interface IAnnouncementRecordRepository : IBaseRepository<AnnouncementsRecords>
{
    Task<List<AnnouncementsRecords>> SearchInAnnouncements(Guid parishId, string query, int page, int pageSize, CancellationToken cancellationToken);
}
