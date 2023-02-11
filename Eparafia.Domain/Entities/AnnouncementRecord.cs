using Shared.BaseModels.BaseEntities;

namespace Eparafia.Domain.Entities;

public class AnnouncementRecord : Entity
{
    public string Content { get; set; }
    public Guid AnnouncementId { get; set; }
    public Announcement? Announcement { get; set; }
}