using Microsoft.Extensions.Caching.Distributed;
using Shared.Service.Interfaces;

namespace Shared.Service.Implementations;

public class AuthorizationCache : IAuthorizationCache
{
    private readonly IDistributedCache _cache;
    
    public AuthorizationCache(IDistributedCache cache) => _cache = cache;

    public Task CreateUser(Guid userId, Guid parishId, CancellationToken cancellationToken = default) => _cache.SetAsync(userId.ToString(), parishId.ToByteArray(), cancellationToken);
    public Task RemoveUser(Guid userId, CancellationToken cancellationToken = default) => _cache.RemoveAsync(userId.ToString(), cancellationToken);
    public async Task<(Guid, Guid)> GetUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var value = await _cache.GetStringAsync(userId.ToString(), cancellationToken);
        if (value is null)
        {
            throw new NullReferenceException();
        }
        return (userId, Guid.Parse(value));
    }

}