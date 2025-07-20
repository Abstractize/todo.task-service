using Models;
using Entities = Data.Entities;

namespace Extensions;

public static class TaskItemEx
{
    public static Entities.TaskItem LoadFrom(this Entities.TaskItem dest, TaskItem src, Guid userId)
    {
        dest.Title = src.Title;

        dest.CompletedAtUtc = src.IsCompleted ? DateTime.UtcNow : null;
        dest.CompletedBy = src.IsCompleted ? userId : null;

        dest.TaskListId = src.TaskListId;

        return dest;
    }

    public static TaskItem LoadFrom(this TaskItem dest, Entities.TaskItem src)
    {
        dest.Id = src.Id;

        dest.Title = src.Title;
        dest.IsCompleted = src.IsCompleted;

        dest.CreatedAtUtc = src.CreatedAtUtc.ToUniversalTime();

        dest.TaskListId = src.TaskListId;

        return dest;
    }
}
