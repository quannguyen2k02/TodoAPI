using DataAccessLayer.Data;
using DataAccessLayer.Enitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TodoRepository:ITodoRepositories
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<TodoItem> AddTodo(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            
            var todo = await _context.TodoItems.FindAsync(id);
            if(todo != null)
            {
                 _context.TodoItems.Remove(todo);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTodoFinished()
        {
            var todos = await _context.TodoItems.Where(x => x.IsCompleted == true).ToListAsync();
            _context.TodoItems.RemoveRange(todos);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<bool> FinishTodo(int id)
        {
            var todo =await _context.TodoItems.FindAsync(id);
            if(todo != null)
            {
                todo.IsCompleted = true;
                _context.TodoItems.Update(todo);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodo()
        {
            var list = await _context.TodoItems.ToListAsync();
            return list;
        }
    }
}
