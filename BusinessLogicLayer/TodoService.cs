using DataAccessLayer.Enitites;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepositories _todoRepo;

        public TodoService(ITodoRepositories todoRepo) {
            _todoRepo = todoRepo;
        }
        public async Task<TodoItem> AddTodo(TodoItem item)
        {
            var i =await _todoRepo.AddTodo(item);
            return i;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            bool result = await _todoRepo.DeleteTodo(id);
            return result;
        }

        public async Task<bool> DeleteTodoFinished()
        {
            bool result = await _todoRepo.DeleteTodoFinished();
            return result;
        }

        public async Task<bool> FinishTodo(int id)
        {
            bool result = await _todoRepo.FinishTodo(id);
            return result;
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            var list =await _todoRepo.GetAllTodo();
            return list;
        }
    }
}
