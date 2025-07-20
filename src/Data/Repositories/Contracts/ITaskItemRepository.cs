using Data.Entities;

namespace Data.Repositories.Contracts;

public interface ITaskItemRepository : IBaseRepository<TaskItem>
{
    Task<TaskItem?> FindFirstActivityTaskItemAsync(Guid userId);
    Task<TaskItem?> FindLastActivityTaskItemAsync(Guid userId);
    Task AddItemAsync(TaskItem dest, Guid userId);
    Task UpdateItemAsync(TaskItem dest, Guid userId);
    Task DeleteItemAsync(Guid itemId, Guid userId);
}