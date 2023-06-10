namespace Shared.Service.Interfaces;

public interface IUserProvider
{
    Guid UserId { get; }
    Guid? ParishId { get; }
    Task SetUser(Guid? id);
}