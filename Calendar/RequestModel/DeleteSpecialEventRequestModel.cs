using eparafia.Helpers;

namespace eparafia.Calendar.RequestModel;

public class DeleteSpecialEventRequestModel : Request
{
    public DeleteSpecialEventRequestModel(int id, string token) : base(token)
    {
        Id = id;
    }

    public int Id { get; }
}