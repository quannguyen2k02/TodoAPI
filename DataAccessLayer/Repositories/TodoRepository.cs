using DataAccessLayer.Data;
using DataAccessLayer.Enitites;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

    public class TodoRepository:ITodoRepositories
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
            var list = await _context.TodoItems.Where(x=>x.IsDeleted == false).ToListAsync();
            return list;
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
            return await _context.TodoItems.Where(x=>!x.IsDeleted.HasValue).ToListAsync();
        }
        public async Task<List<TodoItem>> SearchTasksAsync1(string query)
        {
            var list = new List<TodoItem>();
            var normalizedQuery = query.ToLower(); // Chuẩn hóa từ khóa tìm kiếm

            list = await _context.TodoItems
                .Where(t => t.IsDeleted == false && t.Title.ToLower().Contains(normalizedQuery))
                .ToListAsync();
            return list;
        }
    }

