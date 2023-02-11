using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Shared.Service.Implementations;

public class FileManager : IFileManager
{
    private readonly string minifiedImageName = "-min";

    private readonly string _imagePath;
    private readonly string _imageExtension;
    private int _imageSizeMin { get; set; }
    

    Dictionary<ImageType, string> _DictionaryNames = new Dictionary<ImageType, string>
    {
        {ImageType.AnnouncementsPhoto, "Announcements"},
        {ImageType.ParishAvatar, "Parishes"},
        {ImageType.PostPhoto, "PostPhoto"},
        {ImageType.UserAvatar, "UserAvatar"},
        {ImageType.PriestAvatar, "PriestAvatar"}
    };

    public FileManager(IConfiguration configuration)
    {
        _imagePath = configuration["PathToImages"]!;
        _imageExtension = configuration["ImageExtension"]!;
        _imageSizeMin = int.Parse(configuration["ImageSizeMin"]!);
    }

    public async Task<Tuple<string, string>> SaveImageAsync(string base64, ImageType imageType, Guid imageId, CancellationToken cancellationToken)
    {
        byte[] bytes = Convert.FromBase64String(base64);
        Image image;
        using (MemoryStream ms = new MemoryStream(bytes))
        {
            image = await Image.LoadAsync(ms, cancellationToken);
        }
        
        await image.SaveAsWebpAsync(FactoryFilePath(imageType, imageId), cancellationToken);

        image.Mutate(x => x
            .Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new SixLabors.ImageSharp.Size(_imageSizeMin, _imageSizeMin)
            }));

        await image.SaveAsWebpAsync(FactoryFilePathMin(imageType, imageId), cancellationToken);
        return Tuple.Create(FactoryFilePath(imageType, imageId), FactoryFilePathMin(imageType, imageId));
    }
    public void RemoveImage(ImageType imageType, Guid imageId, CancellationToken cancellationToken)
    {
        File.Delete(FactoryFilePath(imageType, imageId));
        File.Delete(FactoryFilePathMin(imageType, imageId));
    }
    
    private string FactoryFilePath(ImageType imageType, Guid imageId)
    {
        return $"{_imagePath}/{_DictionaryNames[imageType]}/{imageId}.{_imageExtension}";
    }    
    private string FactoryFilePathMin(ImageType imageType, Guid imageId)
    {
        return $"{_imagePath}/{_DictionaryNames[imageType]}/{imageId}{minifiedImageName}.{_imageExtension}";
    }
}