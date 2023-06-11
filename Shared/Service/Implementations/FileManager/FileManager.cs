using System.Net;
using Eparafia.Application.Enums;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Renci.SshNet;
using Shared.BaseModels.LoginObject;
using Shared.Service.Implementations.FileManagerHelper;
using Shared.Service.Interfaces;

namespace Shared.Service.Implementations;

public class FileManager : IFileManager
{
    private readonly IFilePathManager _minFilePathManager;
    private readonly IFilePathManager _normalFilePathManager;
    private readonly IFileManagerHelper _minFileManagerHelper;
    private readonly IFileManagerHelper _normalFileManagerHelper;
    private readonly int _imageSizeMin;

    public FileManager(IConfiguration configuration)
    {
        _imageSizeMin = int.Parse(configuration["ImageSizeMin"]!);
        _minFilePathManager = new FilePathMinPathManager(configuration);
        _normalFilePathManager = new FilePathNormalPathManager(configuration);
        _normalFileManagerHelper = new FileNormalManagerHelper(configuration);
        _minFileManagerHelper = new FileMinManagerHelper(configuration);
    }

    public async Task<Tuple<string, string>> SaveImageAsync(string base64, ImageType imageType, Guid imageId, CancellationToken cancellationToken)
    {
        var bytes = Convert.FromBase64String(base64);
        Image image;
        using (var ms = new MemoryStream(bytes))
        {
            image = await Image.LoadAsync(ms, cancellationToken);
        }

        await image.SaveAsJpegAsync(_normalFilePathManager.FactoryLocalFilePath(imageType, imageId), cancellationToken);
        image.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(_imageSizeMin, _imageSizeMin)
            }));

        await image.SaveAsJpegAsync(_minFilePathManager.FactoryLocalFilePath(imageType, imageId), cancellationToken);

        await _normalFileManagerHelper.UploadFile(imageType, imageId);
        await _minFileManagerHelper.UploadFile(imageType, imageId);
        _minFileManagerHelper.RemoveLocalImage(imageType, imageId);
        _normalFileManagerHelper.RemoveLocalImage(imageType, imageId);
        
        return Tuple.Create(_normalFilePathManager.FactoryOutPutPath(_normalFilePathManager.FactoryFilePath(imageType, imageId), imageType), _minFilePathManager.FactoryOutPutPath(_minFilePathManager.FactoryFilePath(imageType, imageId), imageType));
    }
    public void RemoveImage(ImageType imageType, Guid imageId, CancellationToken cancellationToken)
    {
        File.Delete(_normalFilePathManager.FactoryFilePath(imageType, imageId));
        File.Delete(_minFilePathManager.FactoryFilePath(imageType, imageId));
    }
}