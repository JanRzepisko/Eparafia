using Shared.BaseModels.BaseEntities;

namespace Eparafia.Domain.Entities;

public class Post : Entity
{
    public Guid ParishId { get; set; }
    public Parish Parish { get; set; }
    public DateTime PublishDate { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
    public Priest Author { get; set; }
    public ICollection<PostFile>? Files { get; set; }
}