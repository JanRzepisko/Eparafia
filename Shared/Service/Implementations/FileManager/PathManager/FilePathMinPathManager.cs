using Eparafia.Application.Enums;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.LoginObject;
using Shared.Service.Interfaces;

namespace Shared.Service.Implementations.FileManagerHelper;

public class FilePathMinPathManager : IFilePathManager
{
    private readonly string _imageExtension;
    private readonly string _imagePath;
    private const string MinifiedImageName = "-min";
    public FilePathMinPathManager(IConfiguration configuration)
    {
        _imagePath = configuration["PathToImages"]!;
        _imageExtension = configuration["ImageExtension"]!;
    }
    public string FactoryOutPutPath(string fileName, ImageType imageType) => $"{DictionaryNames.Dictionary[imageType]}{fileName.Split(DictionaryNames.Dictionary[imageType])[1]}";
    public string FactoryFilePath(ImageType imageType, Guid imageId) => $"{_imagePath}/{DictionaryNames.Dictionary[imageType]}/{imageId}{MinifiedImageName}.{_imageExtension}";
    public string FactoryLocalFilePath(ImageType imageType, Guid imageId) => $"{DictionaryNames.Dictionary[imageType]}-{imageId}{MinifiedImageName}.{_imageExtension}";
}