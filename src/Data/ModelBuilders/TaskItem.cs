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

            builder.Property(t => t.CompletedBy)
                .IsRequired(false);
            builder.Property(t => t.CompletedAtUtc)
                .IsRequired(false);

            builder.Property(t => t.CreatedBy)
                .IsRequired();
            builder.Property(t => t.CreatedAtUtc)
                .IsRequired();

            builder.Property(t => t.UpdatedBy)
                .IsRequired();
            builder.Property(t => t.UpdatedAtUtc)
                .IsRequired();

            builder.Property(t => t.DeletedBy)
                .IsRequired(false);
            builder.Property(t => t.DeletedAtUtc)
                .IsRequired(false);

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