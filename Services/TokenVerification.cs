namespace eparafia.Helpers;

public class TokenVerification : ITokenVerification
{
    private readonly ISqlManager _sqlManager;
    
    public TokenVerification(ISqlManager sqlManager)
    {
        _sqlManager = sqlManager;
    }

    public async Task<bool> UserVerification(string? token, UserType type)
    {
        if(type == UserType.User)
            return await _sqlManager.IsValueExist($"SELECT * FROM users.users WHERE token1 = '{token}' OR token2 = '{token}';");
        if(type == UserType.Priest)
            return await _sqlManager.IsValueExist($"SELECT * FROM users.priest WHERE token1 = '{token}' OR token2 = '{token}';");
        return false;
    }
}