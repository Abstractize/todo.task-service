using Models;

namespace Managers.Contracts;

public interface ITaskItemManager
{
    Task AddAsync(TaskItem item);
    Task DeleteAsync(Guid itemId);
    Task<IEnumerable<TaskItem>> GetAsync(Guid taskListId);
    Task<IEnumerable<TaskItem>> GetByTaskListAsync(Guid taskListId);
    Task<IEnumerable<TaskItem>> GetWeeklyTasksAsync(Guid userId, DateTime weekStartDateUtc);
    Task UpdateAsync(Guid itemId, TaskItem item);
}
