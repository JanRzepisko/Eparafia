using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;

namespace Eparafia.Application.Repository;

public interface IPriestRepository : IUserRepository<Priest>
{
    Task<List<Priest>> GetFreePriestAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
}
