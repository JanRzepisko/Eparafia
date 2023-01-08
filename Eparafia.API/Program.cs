using Eparafia.API;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            webBuilder.UseKestrel(c =>
            {
                c.Limits.MaxRequestBodySize = long.MaxValue;
            });
        }).UseDefaultServiceProvider(options => options.ValidateScopes = false);
