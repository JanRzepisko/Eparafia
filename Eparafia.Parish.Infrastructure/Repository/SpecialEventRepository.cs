using Eparafia.Application.Repository;
using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Infrastructure.Repository;

public class SpecialEventRepository : BaseRepository<SpecialEvent>, ISpecialEventRepository
{
    public SpecialEventRepository(DbSet<SpecialEvent>? entities) : base(entities)
    {
    }

    public Task<List<SpecialEvent>?> GetForWeek(Guid requestParishId, DateTime startDate,
        CancellationToken cancellationToken)
    {
        return _entities
            .Where(x => x.ParishId == requestParishId && x.Date > startDate && x.Date < startDate.AddDays(7))
            .ToListAsync(cancellationToken);
    }

    public Task<List<SpecialEvent>?> GetForDay(Guid parishId, DateTime day, CancellationToken cancellationToken)
    {
        return _entities.Where(c =>
            c.Date.Year == day.Year &&
            c.Date.Month == day.Month &&
            c.Date.Day == day.Day &&
            c.ParishId == parishId).ToListAsync(cancellationToken);
    }
}