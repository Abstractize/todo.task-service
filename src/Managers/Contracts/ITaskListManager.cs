using Models;

namespace Managers.Contracts;

public interface ITaskListManager
{
    Task AddAsync(TaskList list);
    Task DeleteAsync(Guid listId);
    Task<TaskList> FindAsync(Guid listId);
    Task<IEnumerable<TaskList>> GetAsync();
    Task UpdateAsync(Guid listId, TaskList list);
}
