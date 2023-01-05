using Eparafia.Application.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Eparafia.Application.DTOs;

public class AnnouncementsRecordDTO
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid AnnouncementId { get; set; }

    public static List<AnnouncementsRecordDTO> FromEntity(List<AnnouncementsRecords> records)
    {
        return records.Select(c => new AnnouncementsRecordDTO
        {
            AnnouncementId = c.AnnouncementId,
            Content = c.Content,
            Id = c.Id
        }).ToList();
    }
}