using eparafia.Helpers;

namespace eparafia.Models;

public class RegisterUserRequestModel : Request
{
    public RegisterUserRequestModel(string name, string surName, string email, string phoneNumber, string password)
    {
        Name = name;
        SurName = surName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public string Name { get; }
    public string SurName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public string Password { get; }

    public override async Task<bool> Validate()
    {
        SqlManager sql = new SqlManager();
        if (await sql.IsValueExist($"SELECT * FROM users.users WHERE email = '{Email}';"))
            return false;
        if (await sql.IsValueExist($"SELECT * FROM users.users WHERE phoneNumber = '{PhoneNumber}';"))
            return false;

        return true;
    }
}