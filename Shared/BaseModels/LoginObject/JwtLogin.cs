using Microsoft.Extensions.Configuration;

namespace Shared.BaseModels.Jwt;

public class JwtLogin
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }

    public static JwtLogin FromConfiguration(IConfiguration configuration)
    {
        return new JwtLogin
        {
            Key = configuration["Jwt:Key"],
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };
    }
}