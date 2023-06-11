using Eparafia.Application.Enums;

namespace Shared.Service.Implementations.FileManagerHelper;

public static class DictionaryNames
{
    public static readonly Dictionary<ImageType, string> Dictionary = new()
    {
        { ImageType.AnnouncementsPhoto, "Announcements" },
        { ImageType.ParishAvatar, "Parishes" },
        { ImageType.PostPhoto, "PostPhoto" },
        { ImageType.UserAvatar, "UserAvatar" },
        { ImageType.PriestAvatar, "PriestAvatar" }
    };
}