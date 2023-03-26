using Eparafia.Domain.Objects;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Application.Repository;

public interface IUserRepository<TEntity> : IBaseRepository<TEntity> where TEntity : UserModel
{
    Task<TEntity?> GetByLoginAsync(string email, CancellationToken cancellationToken = default);
}