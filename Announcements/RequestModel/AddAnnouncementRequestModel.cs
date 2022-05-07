using eparafia.Helpers;

namespace eparafia.Announcements.RequestModel;

public class AddAnnouncementRequestModel : Request
{
    public AddAnnouncementRequestModel(string title, string content, int parafiaId, string token) : base(token)
    {
        Title = title;
        Content = content;
        ParafiaId = parafiaId;
    }

    public int ParafiaId { get; }
    public string Title { get; }
    public string Content { get; }
}