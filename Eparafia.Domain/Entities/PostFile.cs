using Eparafia.Application.Enums;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Domain.Entities;

public class PostFile : Entity
{
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    public string FilePath { get; set; }
    public string FilePathMin { get; set; }
    public FileType FileType { get; set; }
}