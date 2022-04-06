using eparafia.Helpers;

namespace eparafia.Announcements.RequestModel;

public class EditAnnouncementRequestModel : Request
{
    public EditAnnouncementRequestModel(int id, SettingsMode mode, string content)
    {
        Id = id;
        Mode = mode;
        Content = content;
    }

    public int Id { get; }
    public SettingsMode Mode { get; }
    public string Content { get; }
}