using Eparafia.Application.Repository;
using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Infrastructure.Repository;

public class ParishRepository : BaseRepository<Parish>, IParishRepository
{
    public ParishRepository(DbSet<Parish>? entities) : base(entities)
    {
    }

    public Task<Parish?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _entities.Include(c => c.Priests).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public Task<Parish?> GetByShortName(string shortName, CancellationToken cancellationToken)
    {
        return _entities.Include(c => c.Priests).FirstOrDefaultAsync(c => c.ShortName == shortName, cancellationToken);
    }
}