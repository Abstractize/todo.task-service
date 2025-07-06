using Data.Repositories.Contracts;
using Entities = Data.Entities;
using Extensions;
using Managers.Contracts;
using Models;

namespace Managers.Implementation;

internal class TaskItemManager(ITaskItemRepository repository) : ITaskItemManager
{
    private readonly ITaskItemRepository _repository = repository;

    public async Task<IEnumerable<TaskItem>> GetByTaskListAsync(Guid taskListId)
    {
        IEnumerable<Entities.TaskItem> items = await _repository
            .GetAsync(x => x.TaskListId == taskListId && !x.IsCompleted);

        return items
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new TaskItem().LoadFrom(x));
    }

    public async Task AddAsync(TaskItem item)
    {
        //TODO: Validate item

        await _repository.AddAsync(new Entities.TaskItem().LoadFrom(item));
    }

    public async Task UpdateAsync(Guid itemId, TaskItem item)
    {
        // TODO: Validate item

        Entities.TaskItem existingItem = await _repository.FindAsync(x => x.Id == itemId) ??
            throw new ArgumentException("Item not found");

        await _repository.UpdateAsync(existingItem.LoadFrom(item));
    }

    public async Task DeleteAsync(Guid itemId)
    {
        await _repository.DeleteAsync(x => x.Id == itemId);
    }
}