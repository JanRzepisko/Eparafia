using Eparafia.Domain.Objects;

namespace Eparafia.Application.Services.Jwt;

public interface IJwtAuth
{
    public Task<GeneratedToken> GenerateJwt(UserModel user, string role);
}