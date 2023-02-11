using Eparafia.Domain.Entities;

namespace Eparafia.Domain.DTOs;

public class AnnouncementRecordDTO
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid AnnouncementId { get; set; }

    public static List<AnnouncementRecordDTO> FromEntity(List<AnnouncementRecord> records)
    {
        return records.Select(c => new AnnouncementRecordDTO
        {
            AnnouncementId = c.AnnouncementId,
            Content = c.Content,
            Id = c.Id
        }).ToList();
    }
}