using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;
using Eparafia.Application.Repository;
using Microsoft.EntityFrameworkCore;

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