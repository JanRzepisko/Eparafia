using Eparafia.Application.DataAccess.Abstract;

namespace Eparafia.Application.Entities;

public class Announcements : Entity
{
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public ICollection<AnnouncementsRecords> AnnouncementsRecords { get; set; }
}
