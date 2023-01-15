using Eparafia.Application.DataAccess.Abstract;

namespace Eparafia.Application.Entities;

public class AnnouncementRecord : Entity
{
    public string Content { get; set; }
    public Guid AnnouncementId { get; set; }
    public Announcement? Announcement { get; set; }
}