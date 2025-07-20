using Grpc.Services.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc
{
    public static class IServiceCollectionEx
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services)
        {
            services.AddGrpc();
            services.AddScoped<TasksGrpcService>();

            return services;
        }

        public static void MapGrpcServices(this WebApplication app)
        {
            app.MapGrpcService<TasksGrpcService>();
        }
    }
}
