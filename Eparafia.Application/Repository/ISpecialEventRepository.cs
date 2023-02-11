using Eparafia.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Application.Repository;

public interface ISpecialEventRepository : IBaseRepository<SpecialEvent>
{
    Task<List<SpecialEvent>?> GetForWeek(Guid requestParishId,  DateTime startDate, CancellationToken cancellationToken);
    Task<List<SpecialEvent>?> GetForDay(Guid parishId, DateTime day, CancellationToken cancellationToken);
}
