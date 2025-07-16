using Managers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskListController : ControllerBase
{
    private readonly ITaskListManager _taskListManager;

    public TaskListController(ITaskListManager taskListManager)
    {
        _taskListManager = taskListManager;
    }

    [HttpDelete("{listId}")]
    public Task DeleteAsync(Guid listId)
        => _taskListManager.DeleteAsync(listId);

    [HttpGet("{listId}")]
    public Task<TaskList> FindAsync(Guid listId)
        => _taskListManager.FindAsync(listId);

    [HttpGet()]
    public Task<IEnumerable<TaskList>> GetAsync()
        => _taskListManager.GetAsync();

    [HttpPost()]
    public Task PostAsync([FromBody] TaskList list)
        => _taskListManager.AddAsync(list);

    [HttpPut("{listId}")]
    public Task PutAsync(Guid listId, [FromBody] TaskList list)
        => _taskListManager.UpdateAsync(listId, list);
}
