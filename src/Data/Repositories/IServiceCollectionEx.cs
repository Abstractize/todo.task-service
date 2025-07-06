using Data.Repositories.Contracts;
using Data.Repositories.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories
{
    public static class IServiceCollectionEx
    {
        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITaskListRepository, TaskListRepository>();
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();

            return services;
        }
    }
}