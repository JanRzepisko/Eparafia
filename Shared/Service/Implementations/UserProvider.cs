using Shared.Service.Interfaces;

namespace Shared.Service.Implementations;

public class UserProvider : IUserProvider
{
    public Guid Id { get; private set; }

    public async void SetUser(Guid? id)
    {
        if (id != null) Id = (Guid)id;
    }
}