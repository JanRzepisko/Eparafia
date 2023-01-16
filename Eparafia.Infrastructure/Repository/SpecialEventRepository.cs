using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;
using Eparafia.Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eparafia.Infrastructure.Repository;

public class SpecialEventRepository : BaseRepository<SpecialEvent>, ISpecialEventRepository
{
    public SpecialEventRepository(DbSet<SpecialEvent>? entities) : base(entities)
    {
    }

    public Task<List<SpecialEvent>> GetForWeek(Guid requestParishId, DateTime startDate, CancellationToken cancellationToken)
    {
        return _entities.Where(x => x.ParishId == requestParishId && x.Date > startDate).ToListAsync(cancellationToken);
    }
}