using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.ModelBuilders;

public static partial class ModelBuilderEx
{
    public static void BuildTaskItemModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(builder =>
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.IsCompleted)
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.Property(t => t.TaskListId)
                .IsRequired();

            builder.HasOne(t => t.TaskList)
                .WithMany(tl => tl.Tasks)
                .HasForeignKey(t => t.TaskListId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(t => t.TaskListId)
                .HasDatabaseName("IX_TaskItem_TaskListId");

            builder.HasIndex(t => t.TaskListId)
                .HasDatabaseName("IX_TaskItem_TaskListId_Title")
                .IsUnique(false);
        });
    }
}