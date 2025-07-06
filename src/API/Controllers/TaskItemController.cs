using Managers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskItemController : ControllerBase
{
    private readonly ITaskItemManager _taskItemManager;

    public TaskItemController(ITaskItemManager taskItemManager)
    {
        _taskItemManager = taskItemManager;
    }

    [HttpDelete("{itemId}")]
    public Task DeleteAsync(Guid itemId)
        => _taskItemManager.DeleteAsync(itemId);

    [HttpGet("{taskListId}")]
    public Task<IEnumerable<TaskItem>> GetAsync(Guid taskListId)
        => _taskItemManager.GetByTaskListAsync(taskListId);

    [HttpPost()]
    public Task PostAsync([FromBody] TaskItem item)
        => _taskItemManager.AddAsync(item);

    [HttpPut("{itemId}")]
    public Task PutAsync(Guid itemId, [FromBody] TaskItem item)
        => _taskItemManager.UpdateAsync(itemId, item);
}
