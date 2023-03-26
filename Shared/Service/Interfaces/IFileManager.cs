using Eparafia.Application.Enums;

namespace Eparafia.Application.Services.FileManager;

public interface IFileManager
{
    Task<Tuple<string, string>> SaveImageAsync(string base64, ImageType imageType, Guid imageId,
        CancellationToken cancellationToken);

    void RemoveImage(ImageType imageType, Guid imageId, CancellationToken cancellationToken);
}