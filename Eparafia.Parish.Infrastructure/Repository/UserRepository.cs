using Eparafia.Application.Repository;
using Eparafia.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Infrastructure.Repository;

public class UserRepository<TEntity> : BaseRepository<TEntity?>, IUserRepository<TEntity>
    where TEntity : UserModel, new()
{
    public UserRepository(DbSet<TEntity>? entities) : base(entities)
    {
    }

    public Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _entities.Include(c => c.Parish).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}