using System.Net;
using Eparafia.Application.Enums;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Renci.SshNet;
using Shared.BaseModels.LoginObject;
using Shared.Service.Interfaces;

namespace Shared.Service.Implementations;

public class FileManager : IFileManager
{
    private readonly string _imageExtension;
    private readonly FtpLogin _ftpLogin;
    private readonly string _imagePath;
    private const string MinifiedImageName = "-min";


    private readonly Dictionary<ImageType, string> _dictionaryNames = new()
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
        _ftpLogin = FtpLogin.GetFromConfiguration(configuration);
        ImageSizeMin = int.Parse(configuration["ImageSizeMin"]!);
    }

    private int ImageSizeMin { get; }

    public async Task<Tuple<string, string>> SaveImageAsync(string base64, ImageType imageType, Guid imageId, CancellationToken cancellationToken)
    {
        var bytes = Convert.FromBase64String(base64);
        Image image;
        using (var ms = new MemoryStream(bytes))
        {
            image = await Image.LoadAsync(ms, cancellationToken);
        }

        await image.SaveAsJpegAsync(FactoryLocalFilePath(imageType, imageId), cancellationToken);
        image.Mutate(x => x
            .Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(ImageSizeMin, ImageSizeMin)
            }));

        await image.SaveAsJpegAsync(FactoryLocalFilePathMin(imageType, imageId), cancellationToken);

        await UploadFile(imageType, imageId);
        await UploadFileMin(imageType, imageId);
        RemoveLocalImage(imageType, imageId);
        
        return Tuple.Create(FactoryOutPutPath(FactoryFilePath(imageType, imageId), imageType), FactoryOutPutPath(FactoryFilePathMin(imageType, imageId), imageType));
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

    private async Task UploadFile(ImageType imageType, Guid imageId)
    {
        try
        {
            var ftpRequest = (FtpWebRequest)WebRequest.Create($"{_ftpLogin.HostName}/{FactoryFilePath(imageType, imageId)}");
            ftpRequest.Credentials = new NetworkCredential(_ftpLogin.Username, _ftpLogin.Password);
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
        
            var file = await File.ReadAllBytesAsync(FactoryLocalFilePath(imageType, imageId));
            var sr = ftpRequest.GetRequestStream();
            sr.WriteAsync(file, 0, file.Length);
            sr.Close();
            var response = (FtpWebResponse)ftpRequest.GetResponse();
            response.Close();      
        }
        catch(WebException e)
        {
            var status = ((FtpWebResponse)e.Response).StatusDescription;
        }
        //Use ssh to add perms for nginx

        using var ssh = new SshClient("192.168.1.100", "jabuszko", "!Malinka@pass");
        ssh.Connect();
        var path = $"{FactoryFilePath(imageType, imageId)}";
        ssh.RunCommand($"chmod 777 {path}");
        ssh.Disconnect();
    }
    private async Task UploadFileMin(ImageType imageType, Guid imageId)
    {
        var ftpRequest = (FtpWebRequest)WebRequest.Create($"{_ftpLogin.HostName}/{FactoryFilePathMin(imageType, imageId)}");
        ftpRequest.Credentials = new NetworkCredential(_ftpLogin.Username, _ftpLogin.Password);
        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
        
        var file = await File.ReadAllBytesAsync(FactoryLocalFilePathMin(imageType, imageId));
        var sr = ftpRequest.GetRequestStream();
        sr.WriteAsync(file, 0, file.Length);
        sr.Close();
        var response = (FtpWebResponse)ftpRequest.GetResponse();
        response.Close();

        //Use ssh to add perms for nginx

        using var ssh = new SshClient("192.168.1.100", "jabuszko", "!Malinka@pass");
        ssh.Connect();
        var path = $"{FactoryFilePathMin(imageType, imageId)}";
        ssh.RunCommand($"chmod 777 {path}");
        ssh.Disconnect();
    }

    private string FactoryOutPutPath(string fileName, ImageType imageType) => $"{_dictionaryNames[imageType]}{fileName.Split(_dictionaryNames[imageType])[1]}";
    private string FactoryFilePath(ImageType imageType, Guid imageId) => $"{_imagePath}/{_dictionaryNames[imageType]}/{imageId}.{_imageExtension}";
    private string FactoryFilePathMin(ImageType imageType, Guid imageId) => $"{_imagePath}/{_dictionaryNames[imageType]}/{imageId}{MinifiedImageName}.{_imageExtension}";
    private string FactoryLocalFilePath(ImageType imageType, Guid imageId) => $"{_dictionaryNames[imageType]}-{imageId}.{_imageExtension}";
    private string FactoryLocalFilePathMin(ImageType imageType, Guid imageId) => $"{_dictionaryNames[imageType]}-{imageId}{MinifiedImageName}.{_imageExtension}";
}