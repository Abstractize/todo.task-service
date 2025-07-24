using Data;
using Managers;
using Services.Common.Identity;
using API.Common.Middlewares;
using API.Configurations;
using Grpc;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigurePorts();

        string JWT_ISSUER = builder.Configuration.GetValue<string>(nameof(JWT_ISSUER))!;
        string JWT_AUDIENCE = builder.Configuration.GetValue<string>(nameof(JWT_AUDIENCE))!;
        string JWT_KEY = builder.Configuration.GetValue<string>(nameof(JWT_KEY))!;

        string? SQL_CONNECTION_STRING = builder.Configuration
            .GetValue<string>(nameof(SQL_CONNECTION_STRING));

        builder.Services.AddDataLayer(SQL_CONNECTION_STRING);
        builder.Services.AddIdentityService();
        builder.Services.AddManagers();
        builder.Services.AddControllers()
            .AddCamelCaseJson();
        builder.Services.AddGrpcServices();

        builder.Services.AddAuthConfiguration(JWT_ISSUER, JWT_AUDIENCE, JWT_KEY);

        var app = builder.Build();

        app.UseJsonExceptionHandler();
        app.UseAuth();

        app.MapControllers();
        app.MapGrpcServices();

        app.MigrateAtStartup(SQL_CONNECTION_STRING is not null);

        app.Run();
    }
}