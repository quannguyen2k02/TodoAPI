using DataAccessLayer.Data;
using DataAccessLayer.Enitites;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

    public class TodoRepository:ITodoRepository
    {
        private readonly ApplicationDbContext _context;
        
        public TodoRepository(ApplicationDbContext context) {
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
            if(item != null)
            {
                item.IsDeleted = true;
                _context.TodoItems.Update(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<TodoItem>> GetAllTasksAsync()
        {
            return await _context.TodoItems.Where(x=>x.IsDeleted == false).ToListAsync();
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
                var item = list.FirstOrDefault(x=>x.Id == id);
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
            if(item != null)
            {
                item.IsFinished = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<TodoItem>> SearchTasksAsync(string query)
        {
            var normalizedQuery = query.ToLower(); // Chuẩn hóa từ khóa tìm kiếm

            var lists = await _context.TodoItems
                .Where(t => t.IsDeleted==false && t.Title.ToLower().Contains(normalizedQuery))
                .ToListAsync();
            if (lists.Any())
            {
                return lists;
            }
            return await _context.TodoItems.Where(x=>x.IsDeleted == false).ToListAsync();
        }
        

    public async Task<bool> ChangeStatusFinishAsync(int id)
    {
        var changeTask =await _context.TodoItems.FindAsync(id);
        if(changeTask != null)
        {
            changeTask.IsFinished = !changeTask.IsFinished;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;

    }

    public async Task<List<TodoItem>> GetFinishedTasksAsync()
    {
        return await _context.TodoItems.Where(x => x.IsFinished == true && x.IsDeleted == false).ToListAsync();

    }

    public async Task<List<TodoItem>> GetDoingTasksAsync()
    {
        return await _context.TodoItems.Where(x => x.IsFinished == false && x.IsDeleted == false).ToListAsync();
    }
}

