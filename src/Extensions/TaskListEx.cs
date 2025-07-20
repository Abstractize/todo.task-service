using Models;
using Entities = Data.Entities;

namespace Extensions;

public static class TaskListEx
{
    public static Entities.TaskList LoadFrom(this Entities.TaskList dest, TaskList src, Guid? userId = null)
    {
        dest.Title = src.Title;
        dest.Description = src.Description;

        if (userId.HasValue)
            dest.CreatedBy = userId.Value;

        return dest;
    }

    public static TaskList LoadFrom(this TaskList dest, Entities.TaskList src)
    {
        dest.Id = src.Id;
        dest.Title = src.Title;
        dest.Description = src.Description;

        return dest;
    }
}
