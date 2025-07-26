using Data.Common.Context;
using Data.Entities;
using Data.ModelBuilders;
using Microsoft.EntityFrameworkCore;
using Services.Common.Identity;

namespace Data.Context
{
    public class DatabaseContext(
        IIdentityService identityService,
        DbContextOptions<DatabaseContext> options
    ) : BaseContext<DatabaseContext>(identityService, options)
    {
        public DbSet<TaskList> TaskLists => Set<TaskList>();
        public DbSet<TaskItem> TaskItems => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildTaskListModel();
            modelBuilder.BuildTaskItemModel();

            base.OnModelCreating(modelBuilder);
        }
    }
}
