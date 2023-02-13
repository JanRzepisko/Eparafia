using Eparafia.Application.Services.Jwt;
using Eparafia.Domain.Objects;

namespace Eparafia.Identity.Application.Services;

public interface IJwtAuth
{
    public Task<GeneratedToken> GenerateJwt(UserModel user, string role);
}