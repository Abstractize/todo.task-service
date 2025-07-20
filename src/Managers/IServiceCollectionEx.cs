using Managers.Contracts;
using Managers.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Managers
{
    public static class IServiceCollectionEx
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<ITaskListManager, TaskListManager>();
            services.AddScoped<ITaskItemManager, TaskItemManager>();
            services.AddScoped<ITaskLogManager, TaskItemLogManager>();

            return services;
        }
    }
}
