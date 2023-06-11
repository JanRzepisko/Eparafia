using Eparafia.Application.Enums;

namespace Shared.Service.Implementations.FileManagerHelper;

public interface IFileManagerHelper
{
    void RemoveLocalImage(ImageType imageType, Guid imageId);
    Task UploadFile(ImageType imageType, Guid imageId);
}