using Data.Entities;
using Data.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<TaskList> TaskLists => Set<TaskList>();
        public DbSet<TaskItem> TaskItems => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildTaskListModel();
            modelBuilder.BuildTaskItemModel();
        }
    }
}
