using Eparafia.Application.Repository;
using Eparafia.Identity.Domain.Entities;

namespace Eparafia.Identity.Application.DataAccess;

public interface IUnitOfWork
{
    
    IUserRepository<User> Users { get; }
    IUserRepository<Priest> Priests { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}