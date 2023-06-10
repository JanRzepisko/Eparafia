using Eparafia.Application.Repository;
using Eparafia.Identity.Domain.Entities;
using Shared.BaseModels;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Identity.Application.DataAccess;

public interface IUnitOfWork
{
    IUserRepository<User> Users { get; }
    IUserRepository<Priest> Priests { get; }
    IBaseRepository<UserSession> UserSessions { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}