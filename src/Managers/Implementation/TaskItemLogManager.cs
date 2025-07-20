using Data.Repositories.Contracts;
using Managers.Contracts;
using Models;

namespace Managers.Implementation;

public class TaskItemLogManager(ITaskItemRepository repository) : ITaskLogManager
{
    private readonly ITaskItemRepository _repository = repository;

    public async Task<TaskItemLog> GetFirstActivityAsync(Guid userId)
    {
        var taskItem = await _repository
            .FindFirstActivityTaskItemAsync(userId);

        return new TaskItemLog
        {
            Action = LogAction.Created,
            ExecutionAt = taskItem?.CreatedAtUtc ?? DateTime.UtcNow
        };
    }

    public async Task<TaskItemLog> GetLastActivityAsync(Guid userId)
    {
        var taskItem = await _repository
            .FindLastActivityTaskItemAsync(userId);

        return new TaskItemLog
        {
            Action = LogAction.Updated,
            ExecutionAt = taskItem?.UpdatedAtUtc ?? DateTime.UtcNow
        };
    }
}
