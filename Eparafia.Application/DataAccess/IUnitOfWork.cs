using Eparafia.Application.Entities;
using Eparafia.Application.Repository;

namespace Eparafia.Application.DataAccess;

public interface    IUnitOfWork
{
    public IUserRepository<User> Users { get; }
    public IUserRepository<Priest?> Priests { get; }
    public IParishRepository Parishes { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}