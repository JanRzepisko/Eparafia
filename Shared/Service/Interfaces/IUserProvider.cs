namespace Shared.Service.Interfaces;

public interface IUserProvider
{
    Guid Id { get; }
    void SetUser(Guid? id);
}