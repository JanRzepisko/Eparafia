using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;
using Eparafia.Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eparafia.Infrastructure.Repository;

public class IntentionRepository : BaseRepository<Intention>, IIntentionRepository
{
    public IntentionRepository(DbSet<Intention>? entities) : base(entities)
    {
    }

    public Task<Intention?> GetByDateAsync(Guid parishId, DateTime date, CancellationToken cancellationToken)
    {
        return _entities.Where(x => x.ParishId == parishId && x.Date == date).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> ExistContentInDay(Guid parishId, DateTime dateTime, string content, CancellationToken cancellationToken)
    {
        return _entities.AsQueryable()
                        .AnyAsync(c => c.ParishId == parishId
                                       && c.Date.Year == dateTime.Year
                                       && c.Date.Month == dateTime.Month
                                       && c.Date.Day == dateTime.Day
                                       && c.Content == content, cancellationToken);
    }
}