using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.ModelBuilders;

public static partial class ModelBuilderEx
{
    public static void BuildTaskListModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskList>(builder =>
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.CreatedBy)
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.HasMany(t => t.Tasks)
                .WithOne(t => t.TaskList!)
                .HasForeignKey(t => t.TaskListId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(t => t.CreatedBy)
                .HasDatabaseName("IX_TaskList_UserId")
                .IsUnique(false);

            builder.HasIndex(t => t.Id)
                .HasDatabaseName("IX_TaskList_Id")
                .IsUnique();
        });
    }
}