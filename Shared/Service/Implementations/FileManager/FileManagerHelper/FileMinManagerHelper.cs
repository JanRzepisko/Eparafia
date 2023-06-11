using System.Net;
using Eparafia.Application.Enums;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;
using Shared.BaseModels.LoginObject;
using System;

namespace Shared.Service.Implementations.FileManagerHelper;

public class FileMinManagerHelper : IFileManagerHelper
{
    private readonly FtpLogin _ftpLogin;
    private readonly IFilePathManager _minFilePathManager;

    public FileMinManagerHelper(IConfiguration configuration)
    {
        _ftpLogin = FtpLogin.GetFromConfiguration(configuration);
        _minFilePathManager = new FilePathMinPathManager(configuration);
    }
    
    public void RemoveLocalImage(ImageType imageType, Guid imageId)
    {
        File.Delete(_minFilePathManager.FactoryLocalFilePath(imageType, imageId));
    }

    public async Task UploadFile(ImageType imageType, Guid imageId)
    {
        var ftpRequest = (FtpWebRequest)WebRequest.Create($"{_ftpLogin.HostName}/{_minFilePathManager.FactoryFilePath(imageType, imageId)}");
        ftpRequest.Credentials = new NetworkCredential(_ftpLogin.Username, _ftpLogin.Password);
        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
        
        var file = await File.ReadAllBytesAsync(_minFilePathManager.FactoryLocalFilePath(imageType, imageId));
        var sr = ftpRequest.GetRequestStream();
        await sr.WriteAsync(file);
        sr.Close();
        var response = (FtpWebResponse)ftpRequest.GetResponse();
        response.Close();

        //Use ssh to add perms for nginx

        using var ssh = new SshClient("192.168.1.100", "jabuszko", "!Malinka@pass");
        ssh.Connect();
        var path = $"{_minFilePathManager.FactoryFilePath(imageType, imageId)}";
        ssh.RunCommand($"chmod 777 {path}");
        ssh.Disconnect();    
    }
}