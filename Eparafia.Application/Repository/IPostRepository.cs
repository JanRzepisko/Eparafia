using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;

namespace Eparafia.Application.Repository;

public interface IPostRepository : IBaseRepository<Post>
{
    Task<List<Post>?> GetLatestPosts(Guid parishId, int page, int pageSize, CancellationToken cancellationToken);
}
