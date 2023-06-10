using Microsoft.Extensions.Configuration;
using Shared.Service.Interfaces;
using MySql.Data.MySqlClient;
using Shared.BaseModels;

namespace Shared.Service.Implementations;

public class UserProvider : IUserProvider
{
    public Guid UserId { get; private set; }
    public Guid? ParishId { get; private set; }

    private readonly IAuthorizationCache _authorizationCache;
    private readonly IConfiguration _configuration;
    public UserProvider(IAuthorizationCache authorizationCache, IConfiguration configuration)
    {
        _authorizationCache = authorizationCache;
        _configuration = configuration;
    }


    public async Task SetUser(Guid? id)
    {
        if (UserId != Guid.Empty)
            throw new Exception("Token is already set in this session.");
        if(id is null)
            throw new Exception("Token is null.");

        try
        {
            var userParish = await _authorizationCache.GetUser((Guid)id);
            UserId = userParish.Item1;
            ParishId = userParish.Item2;
        }
        catch (Exception e)
        {
            await using var con = new MySqlConnection(_configuration["IdentityConnectionString"]);
            con.Open();

            var sql = $"SELECT UserId, ParishId FROM _UserSessions WHERE UserId = '{id.ToString()}';";
            var cmd = new MySqlCommand(sql, con);

            var user = await cmd.ExecuteReaderAsync();
            if (user.HasRows)
            {
                await _authorizationCache.CreateUser((Guid)id, user.GetGuid(2));
            }
            else
            {
                throw new Exception("User not found.");
            }
        }
    }
}