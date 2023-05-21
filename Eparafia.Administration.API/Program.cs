
using Eparafia.Administration.API;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            webBuilder.UseKestrel(c => { c.Limits.MaxRequestBodySize = long.MaxValue; });
        }).UseDefaultServiceProvider(options => options.ValidateScopes = false);
}