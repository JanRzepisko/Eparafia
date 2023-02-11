using Shared.BaseModels.BaseEntities;

namespace Eparafia.Domain.Entities;

public class Announcement : Entity
{
    public string Title { get; set; }
    public DateTime PublishDate { get; set; }
    public ICollection<AnnouncementRecord> AnnouncementsRecords { get; set; }
    public Guid ParishId { get; set; }
    public Parish Parish { get; set; }
    public Guid AuthorId { get; set; }
    public Priest Author { get; set; }

    public bool IsActive => PublishDate > DateTime.Now;
}
