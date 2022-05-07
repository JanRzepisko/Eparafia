using eparafia.Helpers;

namespace eparafia;

public interface ITokenVerification
{
    public Task<bool> UserVerification(string? token, UserType type);
}