using Eparafia.Bible.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Bible.Application.DataAccess;

public interface    IUnitOfWork
{
    public IBaseRepository<Day> Days { get; }
    public IBaseRepository<Reading> Readings { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}