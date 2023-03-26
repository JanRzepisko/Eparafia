using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Eparafia.Domain.Objects;
using Eparafia.Identity.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Eparafia.Application.Services.Jwt;

public class JwtAuth : IJwtAuth
{
    private readonly IConfiguration _configuration;

    public JwtAuth(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<GeneratedToken> GenerateJwt(UserModel user, string role)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

        //tu add claims
        //TODO GET CONFIGURATION FORM APPSETINGS    
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Name!),
                new Claim("Surname", user.Surname!),
                new Claim("Email", user.Email!),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Audience = _configuration["Jwt:Audience"]!,
            Issuer = _configuration["Jwt:Issuer"]!,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult(new GeneratedToken(tokenHandler.WriteToken(token)));
    }
}