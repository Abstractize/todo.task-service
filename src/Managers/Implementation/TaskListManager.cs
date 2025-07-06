using Data.Repositories.Contracts;
using Extensions;
using Managers.Contracts;
using Models;
using Models.Common.Exceptions;
using Services.Common.Identity;
using Entities = Data.Entities;

namespace Managers.Implementation;

internal class TaskListManager(ITaskListRepository repository, IIdentityService identityService) : ITaskListManager
{
    private readonly IIdentityService _identityService = identityService;
    private readonly ITaskListRepository _repository = repository;

    public async Task<IEnumerable<TaskList>> GetAsync()
    {
        Guid? userId = _identityService.UserId ??
            throw new UnauthorizedException("User is not authenticated.");

        IEnumerable<Entities.TaskList> lists = await _repository.GetAsync(x => x.UserId == userId);

        return lists.Select(x => new TaskList().LoadFrom(x));
    }

    public async Task<TaskList> FindAsync(Guid listId)
    {
        Entities.TaskList result = await _repository.FindAsync(x => x.Id == listId) ??
            throw new NotFoundException(listId);

        return new TaskList()
            .LoadFrom(result);
    }

    public async Task AddAsync(TaskList list)
    {
        //TODO: Validate list
        Guid? userId = _identityService.UserId ??
            throw new UnauthorizedException("User is not authenticated.");

        await _repository.AddAsync(new Entities.TaskList().LoadFrom(list, userId));
    }

    public async Task UpdateAsync(Guid listId, TaskList list)
    {
        // TODO: Validate list

        Entities.TaskList existingList = await _repository.FindAsync(x => x.Id == listId) ??
            throw new NotFoundException(listId);

        await _repository.UpdateAsync(existingList.LoadFrom(list));
    }

    public async Task DeleteAsync(Guid listId)
    {
        await _repository.DeleteAsync(x => x.Id == listId);
    }
}
