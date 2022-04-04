namespace eparafia.Models;

public class JoinIntoParafiaRequestModel
{
    public JoinIntoParafiaRequestModel(int parafiaId, int userId)
    {
        ParafiaId = parafiaId;
        UserId = userId;
    }

    public int ParafiaId { get; }
    public int UserId { get; }
}