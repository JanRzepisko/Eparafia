using eparafia.Helpers;

namespace eparafia.Priest;

public class RegisterPriestRequestModel : Request
{
    public RegisterPriestRequestModel(string name, string surName, string email)
    {
        Name = name;
        SurName = surName;
        Email = email;
    }

    public string Name { get; }
    public string SurName { get; }
    public string Email { get; }
    
    public override async Task<bool> Validate()
    {
        SqlManager sql = new SqlManager();
        if (await sql.IsValueExist($"SELECT * FROM users.priest WHERE email = '{Email}';"))
            return false;
        return true;
    }
}