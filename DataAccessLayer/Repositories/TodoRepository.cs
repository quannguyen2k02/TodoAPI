using DataAccessLayer.Data;
using DataAccessLayer.Enitites;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDbContext _context;

    public TodoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TodoItem> AddTaskAsync(TodoItem item)
    {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }


    public async Task<bool> DeleteTaskAsync(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item is null) return false;

        item.IsDeleted = true;
        _context.TodoItems.Update(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<TodoItem>> GetAllTasksAsync()
    {
        return await _context.TodoItems.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<bool> FinishTasksAsync(int[] ids)
    {
        var list = await _context.TodoItems.ToListAsync();
        foreach (int id in ids)
        {
            var item = list.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsFinished = true;
                _context.TodoItems.Update(item);
            }
        }
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTasksAsync(int[] ids)
    {
        var list = await _context.TodoItems.ToListAsync();
        foreach (int id in ids)
        {
            var item = list.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsDeleted = true;
                _context.TodoItems.Update(item);
            }
        }
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> FinishTaskAsync(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item is null) return false;

        item.IsFinished = true;
        _context.TodoItems.Update(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<TodoItem>> SearchTasksAsync(string? query, string? status)
    {
        // Tạo truy vấn cơ bản, lọc theo trạng thái xóa
        var tasksQuery = _context.TodoItems.Where(t =>!t.IsDeleted);

        // Nếu `query` không null hoặc rỗng, thêm điều kiện lọc theo `query`
        if (!string.IsNullOrEmpty(query))
        {
            var normalizedQuery = query.ToLower(); // Chuẩn hóa từ khóa tìm kiếm
            tasksQuery = tasksQuery.Where(t => t.Title.ToLower().Contains(normalizedQuery));
        }

        // Nếu `status` không null hoặc rỗng, thêm điều kiện lọc theo `status`
        if (!string.IsNullOrEmpty(status))
        {
            if (status.ToLower() == "done")
            {
                tasksQuery = tasksQuery.Where(t => t.IsFinished);
            }
            else if (status.ToLower() == "doing")
            {
                tasksQuery = tasksQuery.Where(t => !t.IsFinished);
            }
        }

        // Trả về danh sách task sau khi áp dụng các điều kiện lọc
        return await tasksQuery.ToListAsync();
    }


    public async Task<bool> ChangeStatusFinishAsync(int id)
    {
        var changeTask = await _context.TodoItems.FindAsync(id);
        if (changeTask is null) return false;

        changeTask.IsFinished = !changeTask.IsFinished;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<TodoItem>> GetFinishedTasksAsync()
    {
        return await _context.TodoItems.Where(x => x.IsFinished && !x.IsDeleted).ToListAsync();

    }

    public async Task<List<TodoItem>> GetDoingTasksAsync()
    {
        return await _context.TodoItems.Where(x => !x.IsFinished && !x.IsDeleted).ToListAsync();
    }

    public async Task<bool> UpdateTaskAsync(TodoItem item)
    {
        _context.TodoItems.Update(item);
        await _context.SaveChangesAsync();
        return true;
    }
}

