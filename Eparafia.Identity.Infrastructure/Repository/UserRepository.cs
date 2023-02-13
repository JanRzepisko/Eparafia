using Eparafia.Application.Repository;
using Eparafia.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Identity.Infrastructure.Repository;

public class UserRepository<TEntity> : BaseRepository<TEntity?>, IUserRepository<TEntity> where TEntity : UserModel, new()
{
    public UserRepository(DbSet<TEntity>? entities) : base(entities)
    {
    }

    public Task<TEntity?> GetByLoginAsync(string email, CancellationToken cancellationToken)
    {
        return _entities.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}