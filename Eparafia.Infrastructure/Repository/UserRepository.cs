using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;
using Eparafia.Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eparafia.Infrastructure.Repository;

public class UserRepository<TEntity> : BaseRepository<TEntity>, IUserRepository<TEntity> where TEntity : UserModel, new()
{
    public UserRepository(DbSet<TEntity>? entities) : base(entities)
    {
    }

    public Task<TEntity?> GetByLoginAsync(string email, CancellationToken cancellationToken)
    {
        return _entities.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}