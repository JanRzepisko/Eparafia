using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;

namespace Eparafia.Application.Repository;

public interface ISpecialEventRepository : IBaseRepository<SpecialEvent>
{
    Task<List<SpecialEvent>?> GetForWeek(Guid requestParishId,  DateTime startDate, CancellationToken cancellationToken);
    Task<List<SpecialEvent>?> GetForDay(Guid parishId, DateTime day, CancellationToken cancellationToken);
}
