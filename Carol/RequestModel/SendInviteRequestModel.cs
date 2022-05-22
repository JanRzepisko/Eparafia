using eparafia.Helpers;

namespace eparafia.Carol.RequestModel;

public class SendInviteRequestModel : Request
{
    public SendInviteRequestModel(int id, string token) : base(token)
    {
        Id = id;
    }

    public int Id { get; }
}