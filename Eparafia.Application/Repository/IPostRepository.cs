using Eparafia.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Application.Repository;

public interface IPostRepository : IBaseRepository<Post>
{
    Task<List<Post>?> GetLatestPosts(Guid parishId, int page, int pageSize, CancellationToken cancellationToken);
}
