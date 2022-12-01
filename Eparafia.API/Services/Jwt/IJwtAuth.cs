namespace Eparafia.API.Services.Jwt;

public interface IJwtAuth
{
    public Task<GeneratedToken> GenerateJwt(Guid id, string role);
}