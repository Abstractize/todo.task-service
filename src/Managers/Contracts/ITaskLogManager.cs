using Models;

namespace Managers.Contracts;

public interface ITaskLogManager
{
    Task<TaskItemLog> GetFirstActivityAsync(Guid userId);
    Task<TaskItemLog> GetLastActivityAsync(Guid userId);
}