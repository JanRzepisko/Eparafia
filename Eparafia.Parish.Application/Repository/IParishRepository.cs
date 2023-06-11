using Eparafia.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Application.Repository;

public interface IParishRepository : IBaseRepository<Domain.Entities.Parish>
{
    Task<Domain.Entities.Parish?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Domain.Entities.Parish?> GetByShortName(string shortName, CancellationToken cancellationToken);
}