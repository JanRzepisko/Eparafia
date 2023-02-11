using Eparafia.Domain.Entities;

namespace Eparafia.Domain.DTOs;

public class PostDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
    public Priest Author { get; set; }
    public ICollection<PostFileDTO> Files { get; set; }
    
    public static PostDTO FromEntity(Post post)
    {
        return new PostDTO
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            AuthorId = post.AuthorId,
            Author = post.Author,
            Files = post.Files.Select(PostFileDTO.FromEntity).ToList()
        };
    }
}