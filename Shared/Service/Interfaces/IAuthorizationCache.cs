namespace Shared.Service.Interfaces;

public interface IAuthorizationCache
{
    Task CreateUser(Guid userId, Guid parishId, CancellationToken cancellationToken = default);
    Task<(Guid, Guid)> GetUser(Guid userId, CancellationToken cancellationToken = default);
    Task RemoveUser(Guid userId, CancellationToken cancellationToken = default);
}