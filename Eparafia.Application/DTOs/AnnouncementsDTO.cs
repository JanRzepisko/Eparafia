using Eparafia.Application.Entities;

namespace Eparafia.Application.DTOs;

public class AnnouncementsDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<AnnouncementsRecordDTO> Records { get; set; }
    public DateTime PublishDate { get; set; }
    
    public static List<AnnouncementsDTO> FromEntity(List<Announcement> announcements)
    {
        return announcements.Select(c => new AnnouncementsDTO
        {
            Id = c.Id,
            Title = c.Title,
            Records = c.AnnouncementsRecords.Select(x => new AnnouncementsRecordDTO
            {
                Id = x.Id,
                Content = x.Content,
                AnnouncementId = x.AnnouncementId
            }).ToList(),
            PublishDate = c.PublishDate
        }).ToList();
    }
}