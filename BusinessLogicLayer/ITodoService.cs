using DataAccessLayer.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface ITodoService
    {
        public Task<TodoItem> AddTodo(TodoItem item);
        public Task<bool> FinishTodo(int id);
        public Task<bool> DeleteTodo(int id);
        public Task<bool> DeleteTodoFinished();
        public Task<IEnumerable<TodoItem>> GetAll();
    }
}
