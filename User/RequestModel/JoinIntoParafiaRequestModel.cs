using eparafia.Helpers;

namespace eparafia.Models;

public class JoinIntoParafiaRequestModel : Request
{
    public JoinIntoParafiaRequestModel(int parafiaId, int userId, string token) : base(token)
    {
        ParafiaId = parafiaId;
        UserId = userId;
    }

    public int ParafiaId { get; }
    public int UserId { get; }
}