namespace Eparafia.Application.DataAccess.Abstract;

public class UserModel : Entity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
}