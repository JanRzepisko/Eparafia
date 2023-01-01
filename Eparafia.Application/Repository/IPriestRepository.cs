using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;

namespace Eparafia.Application.Repository;

public interface IUserRepository<TEntity> : IBaseRepository<TEntity> where TEntity : UserModel
{
    Task<TEntity?> GetByLoginAsync(string email, CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

}
