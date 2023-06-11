using Eparafia.Application.Enums;

namespace Shared.Service.Implementations.FileManagerHelper;

public interface IFilePathManager
{
    string FactoryOutPutPath(string fileName, ImageType imageType);
    string FactoryFilePath(ImageType imageType, Guid imageId);
    string FactoryLocalFilePath(ImageType imageType, Guid imageId);
}