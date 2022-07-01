namespace eparafia.Helpers;

public class TokenVerification : ITokenVerification
{
    private static Random random = new Random();
    private readonly ISqlManager _sqlManager;
    
    public TokenVerification(ISqlManager sqlManager)
    {
        _sqlManager = sqlManager;
    }

    public async Task<bool> UserVerification(string? token, UserType type)
    {
        if(type == UserType.User)
            return await _sqlManager.IsValueExist($"SELECT * FROM users.users WHERE (token1 = '{token}' OR token2 = '{token}') AND id = {int.Parse(token.Split('_')[1])};");
        if(type == UserType.Priest)
            return await _sqlManager.IsValueExist($"SELECT * FROM users.priest WHERE (token1 = '{token}' OR token2 = '{token}') AND id = {int.Parse(token.Split('_')[1])};");
        return false;
    }

    public async Task<string> GenerateToken(int userId)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string newToken = new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        newToken += "_" + userId;

        newToken += "_" + DateTime.Now.ToString("dd-MM-yyyy HH':'mm':'ss");

        var tokens = (await _sqlManager.Reader($"SELECT token1, token2 FROM users.users WHERE id = {userId};"))[0];

        
        if (tokens["token1"] == "")
        {
            await _sqlManager.Execute($"UPDATE users.users SET token1 = '{newToken}' WHERE id = {userId};");
        }
        else if (tokens["token2"] == "")
        {
            await _sqlManager.Execute($"UPDATE users.users SET token2 = '{newToken}' WHERE id = {userId};");
        }
        else if (DateTime.Parse(tokens["token1"].ToString().Split('_')[2]) > DateTime.Parse(tokens["token2"].ToString().Split('_')[2]))
        {
            await _sqlManager.Execute($"UPDATE users.users SET token2 = '{newToken}' WHERE id = {userId};");
        }
        else
        {
            await _sqlManager.Execute($"UPDATE users.users SET token1 = '{newToken}' WHERE id = {userId};");
        }


        return newToken;
    }
}