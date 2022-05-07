using eparafia.Helpers;

namespace eparafia.Announcements.RequestModel;

public class GetAnnouncementRequestModel :  Request
{
    public GetAnnouncementRequestModel(int parafiaId, int page, string token) : base(token)
    {
        ParafiaId = parafiaId;
        Page = page;
    }

    public int ParafiaId { get; }
    public int Page { get; }
}