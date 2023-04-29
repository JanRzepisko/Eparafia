using Microsoft.Extensions.Configuration;

namespace Shared.BaseModels.LoginObject;

public class FtpLogin
{
    public string Username { get; private init; }
    public string Password { get; private init; }
    public string HostName { get; private init; }

    public static FtpLogin GetFromConfiguration(IConfiguration cfg) =>
        new()
        {
            Username = cfg["Ftp:UserName"]!,
            Password = cfg["Ftp:Password"]!,
            HostName = cfg["Ftp:HostName"]!,
        };
}