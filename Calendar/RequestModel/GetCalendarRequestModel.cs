using eparafia.Helpers;

namespace eparafia.Calendar.RequestModel;

public class GetCalendarRequestModel : Request
{
    public GetCalendarRequestModel(int parafiaId, string token) : base(token)
    {
        ParafiaId = parafiaId;
    }

    public int ParafiaId { get; }
}