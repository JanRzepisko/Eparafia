using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;
using Eparafia.Application.Repository;
using Microsoft.EntityFrameworkCore;

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
}