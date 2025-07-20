using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace API.Configurations;

public static class WebHostEx
{
    public static ConfigureWebHostBuilder ConfigurePorts(this ConfigureWebHostBuilder webHost)
    {
        webHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(8080, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1;
            });
            options.ListenAnyIP(5000, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });

        return webHost;
    }
}

