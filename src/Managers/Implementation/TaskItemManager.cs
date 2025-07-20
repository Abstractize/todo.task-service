using Data.Repositories.Contracts;
using Extensions;
using Managers.Contracts;
using Models;
using Models.Common.Exceptions;
using Services.Common.Identity;
using Entities = Data.Entities;

namespace Managers.Implementation;

internal class TaskItemManager(ITaskItemRepository repository, IIdentityService identityService) : ITaskItemManager
{
    private readonly ITaskItemRepository _repository = repository;
    private readonly IIdentityService _identityService = identityService;

    public async Task<IEnumerable<TaskItem>> GetByTaskListAsync(Guid taskListId)
    {
        IEnumerable<Entities.TaskItem> items = await _repository
            .GetAsync(x => x.TaskListId == taskListId && !x.IsCompleted);

        return items
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new TaskItem().LoadFrom(x));
    }

    public async Task<IEnumerable<TaskItem>> GetWeeklyTasksAsync(Guid userId, DateTime weekStartDateUtc)
    {
        // Guid userId = _identityService.UserId ??
        //     throw new UnauthorizedException("User is not authenticated.");

        //TODO: Validate item

        IEnumerable<Entities.TaskItem> createdItemsOnWeek = await _repository
            .GetAsync(x =>
                x.CreatedBy == userId &&
                x.CreatedAtUtc.Date >= weekStartDateUtc &&
                x.CreatedAtUtc.Date < weekStartDateUtc.AddDays(7)
            );

        IEnumerable<Entities.TaskItem> completedItemsOnWeek = await _repository
            .GetAsync(x =>
                x.CompletedAtUtc.HasValue &&
                x.CompletedBy == userId &&
                x.CompletedAtUtc.Value.Date >= weekStartDateUtc &&
                x.CompletedAtUtc.Value.Date < weekStartDateUtc.AddDays(7)
            );

        IEnumerable<Entities.TaskItem> items = createdItemsOnWeek
            .Concat(completedItemsOnWeek)
            .DistinctBy(x => x.Id);

        return items
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new TaskItem().LoadFrom(x));
    }

    public async Task<IEnumerable<TaskItem>> GetAsync(Guid userId)
    {
        // Guid userId = _identityService.UserId ??
        //     throw new UnauthorizedException("User is not authenticated.");

        IEnumerable<Entities.TaskItem> items = await _repository
            .GetAsync(x => x.CreatedBy == userId);

        return items
            .Select(x => new TaskItem().LoadFrom(x));
    }

    public async Task AddAsync(TaskItem item)
    {
        //TODO: Validate item

        Guid userId = _identityService.UserId ??
            throw new UnauthorizedException("User is not authenticated.");

        await _repository.AddItemAsync(
            new Entities.TaskItem()
            .LoadFrom(item, userId),
        userId);
    }

    public async Task UpdateAsync(Guid itemId, TaskItem item)
    {
        // TODO: Validate item

        Guid userId = _identityService.UserId ??
            throw new UnauthorizedException("User is not authenticated.");

        Entities.TaskItem existingItem = await _repository.FindAsync(x => x.Id == itemId) ??
            throw new ArgumentException("Item not found");

        await _repository.UpdateItemAsync(existingItem.LoadFrom(item, userId), userId);
    }

    public async Task DeleteAsync(Guid itemId)
    {
        Guid userId = _identityService.UserId ??
            throw new UnauthorizedException("User is not authenticated.");

        Entities.TaskItem existingItem = await _repository.FindAsync(x => x.Id == itemId) ??
            throw new NotFoundException(itemId);

        await _repository.DeleteItemAsync(itemId, userId);
    }
}
