using Eparafia.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Application.Repository;

public interface IIntentionRepository : IBaseRepository<Intention>
{
    Task<Intention?> GetByDateAsync(Guid parishId, DateTime date, CancellationToken cancellationToken);
    Task<bool> ExistContentInDay(Guid parishId, DateTime dateTime, string content, CancellationToken cancellationToken);
}