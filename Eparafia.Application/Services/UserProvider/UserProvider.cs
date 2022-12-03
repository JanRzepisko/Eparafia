namespace Eparafia.Application.Services.UserProvider;

public class UserProvider : IUserProvider
{
    public Guid Id { get; set; }
    public void SetUser(Guid id)
    {
        if (Id != Guid.Empty)
        {
            throw new ("Token is already set in this session.");
        }
        Id = id;    
    }
}