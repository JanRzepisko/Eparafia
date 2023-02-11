using Eparafia.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Application.Repository;

public interface ICommonWeekRepository : IBaseRepository<CommonEvent>
{
    Task<List<CommonEvent>> GetByParishId(Guid id, CancellationToken cancellationToken);
}
