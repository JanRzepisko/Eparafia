namespace Eparafia.Application.Services.FileManager;

public class PhotoPath : ValueObject
{
    public PhotoPath(string path, string pathMin)
    {
        PathMin = pathMin;
        Path = path;
    }

    public PhotoPath()
    {
        PathMin = string.Empty;
        Path = string.Empty;
    }

    public string Path { get; set; }
    public string PathMin { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Path;
        yield return PathMin;
    }
}