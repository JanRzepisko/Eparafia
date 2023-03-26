using Eparafia.Application.Repository;
using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eparafia.Infrastructure.Repository;

public class PriestRepository : UserRepository<Priest>, IPriestRepository
{
    public PriestRepository(DbSet<Priest>? entities) : base(entities)
    {
    }

    public Task<List<Priest>> GetFreePriestAsync(string query, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        if (query != string.Empty)
            return _entities
                .Where(c => c.Name.Contains(query.ToLower()) && c.ParishId == null)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        return _entities
            .Where(c => c.ParishId == null)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}