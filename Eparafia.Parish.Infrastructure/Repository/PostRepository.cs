using Eparafia.Application.Repository;
using Eparafia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Infrastructure.Repository;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    public PostRepository(DbSet<Post>? entities) : base(entities)
    {
    }

    public Task<List<Post>?> GetLatestPosts(Guid parishId, int page, int pageSize, CancellationToken cancellationToken)
    {
        return _entities.Where(c => c.ParishId == parishId)
            .OrderByDescending(c => c.PublishDate)
            .Include(c => c.Author)
            .Include(c => c.Files)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
    
    public ValueTask<Post?> GetByIdAsync(Guid? id, CancellationToken cancellationToken = default)
    {
        return new ValueTask<Post?>(_entities
            .Include(c => c.Author)
            .Include(c => c.Files)
            .FirstOrDefaultAsync(c => id == c.Id, cancellationToken));
    }
}