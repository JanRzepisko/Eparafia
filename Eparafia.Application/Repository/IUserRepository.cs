using Eparafia.Domain.Objects;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Application.Repository;

public interface IUserRepository<TEntity> : IBaseRepository<TEntity> where TEntity : UserModel
{
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

}
