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
                        .Skip(page * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken);
    }
}