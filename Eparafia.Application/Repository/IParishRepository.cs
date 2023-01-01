using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;

namespace Eparafia.Application.Repository;

public interface IParishRepository : IBaseRepository<Parish>
{
    Task<Parish?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
