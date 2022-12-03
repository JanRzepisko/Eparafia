namespace Eparafia.Application.Services.UserProvider;

public interface IUserProvider
{
    Guid Id { get; }
    void SetUser(Guid id);
}