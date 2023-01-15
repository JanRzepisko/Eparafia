using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;

namespace Eparafia.Application.Repository;

public interface ICommonWeekRepository : IBaseRepository<CommonEvent>
{
    Task<List<CommonEvent>> GetByParishId(Guid id, CancellationToken cancellationToken);
}
