using Eparafia.Application.Enums;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Services.FileManager;

public class FileManager : IFileManager
{
    private readonly string _imagePath;
    private readonly string _imageExtension;

    public FileManager(IConfiguration configuration)
    {
        _imagePath = configuration["PathToImages"]!;
        _imageExtension = configuration["ImageExtension"]!;
    }
    
    public async Task SaveImageAsync(string base64, ImageType imageType, Guid imageId, CancellationToken cancellationToken)
    {
        switch (imageType)
        {
            case ImageType.AnnouncementsPhoto:
            {
                await File.WriteAllBytesAsync($"{_imagePath}/Announcements/{imageId}.{_imageExtension}", Convert.FromBase64String(base64), cancellationToken);
                break;
            }
            case ImageType.ParishAvatar:
            {
                await File.WriteAllBytesAsync($"{_imagePath}/Parishes/{imageId}.{_imageExtension}", Convert.FromBase64String(base64), cancellationToken);
                break;
            }
            case ImageType.PostPhoto:
            {
                await File.WriteAllBytesAsync($"{_imagePath}/PostPhoto/{imageId}.{_imageExtension}", Convert.FromBase64String(base64), cancellationToken);
                break;
            }
            case ImageType.UserAvatar:
            {
                await File.WriteAllBytesAsync($"{_imagePath}/UserAvatar/{imageId}.{_imageExtension}", Convert.FromBase64String(base64), cancellationToken);
                break;
            }            
            case ImageType.PriestAvatar:
            {
                await File.WriteAllBytesAsync($"{_imagePath}/PriestAvatar/{imageId}.{_imageExtension}", Convert.FromBase64String(base64), cancellationToken);
                break;
            }
        }
    }

    public void RemoveImage(ImageType imageType, Guid imageId, CancellationToken cancellationToken)
    {
        switch (imageType)
        {
            case ImageType.AnnouncementsPhoto:
            {
                File.Delete($"{_imagePath}/Announcements/{imageId}.{_imageExtension}");
                break;
            }
            case ImageType.ParishAvatar:
            {
                File.Delete($"{_imagePath}/ParishAvatar/{imageId}.{_imageExtension}");
                break;
            }
            case ImageType.PostPhoto:
            {
                File.Delete($"{_imagePath}/PostPhoto/{imageId}.{_imageExtension}");
                break;
            }
            case ImageType.UserAvatar:
            {
                File.Delete($"{_imagePath}/UserAvatar/{imageId}.{_imageExtension}");
                break;
            }
            case ImageType.PriestAvatar:
            {
                File.Delete($"{_imagePath}/PriestAvatar/{imageId}.{_imageExtension}");
                break;
            }
        }
    }
}