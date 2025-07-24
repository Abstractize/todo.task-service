using Data.Common.Extensions;
using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Data
{
    public static class IServiceCollectionEx
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, string? connectionString = null)
        {
            services.AddDatabaseContext(connectionString);
            services.AddRepositories();

            return services;
        }

        public static void MigrateAtStartup(this IHost host, bool applyMigrations = true)
        {
            if (applyMigrations)
                host.ApplyMigrations<DatabaseContext>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, string? connectionString = null)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    options.UseInMemoryDatabase("DefaultDatabase");
                }
                else
                {
                    options.UseNpgsql(connectionString);
                }
            });

            return services;
        }
    }
}