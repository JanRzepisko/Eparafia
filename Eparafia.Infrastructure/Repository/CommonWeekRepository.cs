using Eparafia.Application.Repository;
using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Infrastructure.Repository;

public class CommonWeekRepository : BaseRepository<CommonEvent>, ICommonWeekRepository
{
    public CommonWeekRepository(DbSet<CommonEvent>? entities) : base(entities)
    {
    }
 

    public Task<List<CommonEvent>> GetByParishId(Guid id, CancellationToken cancellationToken)
    {
        return _entities.Where(c => c.ParishId == id).ToListAsync(cancellationToken);
    }
}