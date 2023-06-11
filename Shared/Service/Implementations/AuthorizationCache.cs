using Microsoft.Extensions.Caching.Distributed;
using Shared.Service.Interfaces;
using StackExchange.Redis;
using TaskExtensions = System.Data.Entity.SqlServer.Utilities.TaskExtensions;

namespace Shared.Service.Implementations;

public class AuthorizationCache : IAuthorizationCache
{
    private readonly IDatabase _cache;
    
    public AuthorizationCache(IConnectionMultiplexer redis)
    {
        _cache = redis.GetDatabase();
        var pong = _cache.PingAsync();
    }

    public Task CreateUser(Guid userId, Guid parishId, CancellationToken cancellationToken = default) => _cache.StringSetAsync(new RedisKey(userId.ToString()), new RedisValue(parishId.ToString()));
    public Task RemoveUser(Guid userId, CancellationToken cancellationToken = default) => _cache.KeyDeleteAsync(new RedisKey(userId.ToString()));
    public async Task<(Guid, Guid)> GetUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var value = await _cache.StringGetAsync(userId.ToString());
        return (userId, Guid.Parse(value));
    }
}