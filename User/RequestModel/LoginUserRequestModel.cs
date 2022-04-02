using eparafia.Helpers;

namespace eparafia.Models;

public class LoginUserRequestModel : Request
{
    public string Email { get; }
    public string Password { get; }

    public LoginUserRequestModel(string email, string password)
    {
        Email = email;
        Password = password;
    }
    
}