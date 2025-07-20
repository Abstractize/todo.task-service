using System.Linq.Expressions;
using Data.Context;
using Data.Entities;
using Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementation;

internal class TaskItemRepository(DatabaseContext context) : BaseRepository<TaskItem>(context), ITaskItemRepository
{
    public async Task<IList<TaskItem>> GetAllCreatedTasksOnWeekAsync(Guid userId)
    {
        return await _context.Set<TaskItem>()
            .AsNoTracking()
            .Where(x => x.CreatedBy == userId && x.CreatedAtUtc >= DateTime.UtcNow.AddDays(-7))
            .ToListAsync();
    }

    public async Task<IList<TaskItem>> GetAllCompletedTasksOnWeekAsync(Guid userId)
    {
        return await _context.Set<TaskItem>()
            .AsNoTracking()
            .Where(x => x.CompletedBy == userId && x.CompletedAtUtc.HasValue)
            .ToListAsync();
    }

    public async Task<TaskItem?> FindFirstActivityTaskItemAsync(Guid userId)
    {
        return await _context.Set<TaskItem>()
            .AsNoTracking()
            .Where(x => x.CreatedBy == userId)
            .OrderBy(x => x.CreatedAtUtc)
            .FirstOrDefaultAsync();
    }

    public async Task<TaskItem?> FindLastActivityTaskItemAsync(Guid userId)
    {
        return await _context.Set<TaskItem>()
            .AsNoTracking()
            .Where(x => x.CreatedBy == userId)
            .OrderByDescending(x => x.UpdatedAtUtc)
            .FirstOrDefaultAsync();
    }

    public async Task AddItemAsync(TaskItem dest, Guid userId)
    {
        dest.CreatedBy = userId;
        dest.UpdatedBy = userId;

        await base.AddAsync(dest);
    }

    public async Task UpdateItemAsync(TaskItem dest, Guid userId)
    {
        dest.UpdatedBy = userId;
        dest.UpdatedAtUtc = DateTime.UtcNow;

        await base.UpdateAsync(dest);
    }

    public async Task DeleteItemAsync(Guid itemId, Guid userId)
    {
        var item = await _context.Set<TaskItem>()
            .FirstOrDefaultAsync(x => x.Id == itemId && x.CreatedBy == userId);
        if (item == null)
        {
            throw new ArgumentException("Item not found");
        }

        item.DeletedAtUtc = DateTime.UtcNow;
        item.DeletedBy = userId;

        await base.UpdateAsync(item);

        await base.DeleteAsync(x => x.Id == itemId);
    }
}
