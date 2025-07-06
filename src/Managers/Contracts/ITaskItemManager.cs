using Models;

namespace Managers.Contracts;

public interface ITaskItemManager
{
    Task AddAsync(TaskItem item);
    Task DeleteAsync(Guid itemId);
    Task<IEnumerable<TaskItem>> GetByTaskListAsync(Guid taskListId);
    Task UpdateAsync(Guid itemId, TaskItem item);
}
