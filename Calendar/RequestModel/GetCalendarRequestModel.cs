using eparafia.Helpers;

namespace eparafia.Calendar.RequestModel;

public class GetCalendarRequestModel : Request
{
    public GetCalendarRequestModel(int parafiaId, int week, string token) : base(token)
    {
        Week = week;
        ParafiaId = parafiaId;
    }

    public int ParafiaId { get; }
    public int Week { get; }
}