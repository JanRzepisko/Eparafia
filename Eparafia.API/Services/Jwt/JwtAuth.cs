using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Eparafia.API.Services.Jwt;

public class JwtAuth : IJwtAuth
{
    private readonly IConfiguration _configuration;
    
    public JwtAuth(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<GeneratedToken> GenerateJwt(Guid id, string role)
    {
        byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

        //tu add claims

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", id.ToString()),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Audience = _configuration["Jwt:Audience"]!,
            Issuer = _configuration["Jwt:Issuer"]!,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult(new GeneratedToken(tokenHandler.WriteToken(token)));
    }
}