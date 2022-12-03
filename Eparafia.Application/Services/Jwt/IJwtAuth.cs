using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Entities;

namespace Eparafia.Application.Services.Jwt;

public interface IJwtAuth
{
    public Task<GeneratedToken> GenerateJwt(UserModel user);
}