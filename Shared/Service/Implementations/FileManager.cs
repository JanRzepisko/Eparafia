using System.Net;
using System.Text;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using Microsoft.Extensions.Configuration;
using Rebex.Net;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;

namespace Shared.Service.Implementations;

public class FileManager : IFileManager
{
    private readonly string _imageExtension;

    private readonly string _imagePath;
    private readonly string minifiedImageName = "-min";


    private readonly Dictionary<ImageType, string> _DictionaryNames = new()
    {
        { ImageType.AnnouncementsPhoto, "Announcements" },
        { ImageType.ParishAvatar, "Parishes" },
        { ImageType.PostPhoto, "PostPhoto" },
        { ImageType.UserAvatar, "UserAvatar" },
        { ImageType.PriestAvatar, "PriestAvatar" }
    };

    public FileManager(IConfiguration configuration)
    {
        _imagePath = configuration["PathToImages"]!;
        _imageExtension = configuration["ImageExtension"]!;
        _imageSizeMin = int.Parse(configuration["ImageSizeMin"]!);
    }

    private int _imageSizeMin { get; }

    public async Task<Tuple<string, string>> SaveImageAsync(string base64, ImageType imageType, Guid imageId, CancellationToken cancellationToken)
    {
        var bytes = Convert.FromBase64String(base64);
        Image image;
        using (var ms = new MemoryStream(bytes))
        {
            image = await Image.LoadAsync(ms, cancellationToken);
        }

        await image.SaveAsWebpAsync(FactoryLocalFilePath(imageType, imageId), cancellationToken);

        image.Mutate(x => x
            .Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(_imageSizeMin, _imageSizeMin)
            }));

        await image.SaveAsWebpAsync(FactoryLocalFilePathMin(imageType, imageId), cancellationToken);

        await UploadFiles(imageType, imageId);
        RemoveLocalImage(imageType, imageId);
        
        return Tuple.Create(FactoryFilePath(imageType, imageId), FactoryFilePathMin(imageType, imageId));
    }

    public void RemoveImage(ImageType imageType, Guid imageId, CancellationToken cancellationToken)
    {
        File.Delete(FactoryFilePath(imageType, imageId));
        File.Delete(FactoryFilePathMin(imageType, imageId));
    }

    private void RemoveLocalImage(ImageType imageType, Guid imageId)
    {
        File.Delete(FactoryLocalFilePath(imageType, imageId));
        File.Delete(FactoryLocalFilePathMin(imageType, imageId));
    }

    private async Task UploadFiles(ImageType imageType, Guid imageId)
    {
        
        var ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://192.168.1.100:21/../../var/www/html/eparafia");

        ftpRequest.Credentials = new NetworkCredential("malinkaftp", "!Malinka@pass");
        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

        byte[] fileContent;

        using (var sr = new StreamReader(FactoryLocalFilePath(imageType, imageId)))
        {
            fileContent = Encoding.UTF8.GetBytes(await sr.ReadToEndAsync()); 
        }

        await using (var sw = ftpRequest.GetRequestStream())
        {
            await sw.WriteAsync(fileContent);
        }

        await ftpRequest.GetResponseAsync();
    }

    private string FactoryFilePath(ImageType imageType, Guid imageId) => $"{_imagePath}/{_DictionaryNames[imageType]}/{imageId}.{_imageExtension}";

    private string FactoryFilePathMin(ImageType imageType, Guid imageId) => $"{_imagePath}/{_DictionaryNames[imageType]}/{imageId}{minifiedImageName}.{_imageExtension}";
    private string FactoryLocalFilePath(ImageType imageType, Guid imageId) => $"/{_DictionaryNames[imageType]}-{imageId}.{_imageExtension}";

    private string FactoryLocalFilePathMin(ImageType imageType, Guid imageId) => $"{_DictionaryNames[imageType]}-{imageId}{minifiedImageName}.{_imageExtension}";
}