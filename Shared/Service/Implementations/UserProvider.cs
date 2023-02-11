using Shared.Service.Interfaces;

namespace Shared.Service.Implementations;

public class UserProvider : IUserProvider
{
    public Guid Id { get; private set; }
    public void SetUser(Guid id)
    {
        if (Id != Guid.Empty)
        {
            throw new ("Token is already set in this session.");
        }
        Id = id;    
    }
}