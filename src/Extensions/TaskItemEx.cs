using Models;
using Entities = Data.Entities;

namespace Extensions;

public static class TaskItemEx
{
    public static Entities.TaskItem LoadFrom(this Entities.TaskItem dest, TaskItem src)
    {
        dest.Title = src.Title;
        dest.IsCompleted = src.IsCompleted;

        dest.TaskListId = src.TaskListId;

        return dest;
    }

    public static TaskItem LoadFrom(this TaskItem dest, Entities.TaskItem src)
    {
        dest.Id = src.Id;
        dest.Title = src.Title;
        dest.IsCompleted = src.IsCompleted;

        dest.TaskListId = src.TaskListId;

        return dest;
    }
}
