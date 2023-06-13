using Eparafia.Application.Repository;
using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Infrastructure.Repository;

public class ParishRepository : BaseRepository<Domain.Entities.Parish>, IParishRepository
{
    public ParishRepository(DbSet<Domain.Entities.Parish>? entities) : base(entities)
    {
    }

    public Task<Domain.Entities.Parish?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _entities.Include(c => c.Priests).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public Task<Domain.Entities.Parish?> GetByShortName(string shortName, CancellationToken cancellationToken)
    {
        return _entities.Include(c => c.Priests).FirstOrDefaultAsync(c => c.ShortName == shortName, cancellationToken);
    }
}