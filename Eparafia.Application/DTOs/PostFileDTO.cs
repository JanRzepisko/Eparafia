using Eparafia.Application.Entities;
using Eparafia.Application.Enums;

namespace Eparafia.Application.DTOs;

public class PostFileDTO
{
    public Guid Id { get; set; }
    public string FilePath { get; set; }
    public string FilePathMin { get; set; }
    public FileType FileType { get; set; }
    
    public static PostFileDTO FromEntity(PostFile postFile)
    {
        return new PostFileDTO
        {
            Id = postFile.Id,
            FilePath = postFile.FilePath,
            FilePathMin = postFile.FilePathMin,
            FileType = postFile.FileType
        };
    }
}