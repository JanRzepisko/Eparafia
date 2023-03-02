using Eparafia.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Application.Repository;

public interface IParishRepository : IBaseRepository<Parish>
{
    Task<Parish?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Parish?> GetByShortName(string shortName, CancellationToken cancellationToken);
}
