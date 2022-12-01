namespace Eparafia.API.Services.UserProvider;

public interface IUserProvider
{
    Guid Id { get; }
    void SetUser(Guid id);
}