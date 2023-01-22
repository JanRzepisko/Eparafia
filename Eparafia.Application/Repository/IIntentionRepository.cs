using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;

namespace Eparafia.Application.Repository;

public interface IIntentionRepository : IBaseRepository<Intention>
{
    Task<Intention?> GetByDate(Guid parishId, DateTime date, CancellationToken cancellationToken);
    Task<bool> ExistContentInDay(Guid parishId, DateTime dateTime, string content, CancellationToken cancellationToken);
}
