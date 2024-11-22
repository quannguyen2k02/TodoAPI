using DataAccessLayer.Enitites;

namespace DataAccessLayer.Repositories;

public interface ITodoRepositories
{
    public Task<TodoItem> AddTaskAsync(TodoItem item);
    public Task<bool> FinishTasksAsync(int[] ids);
    public Task<bool> FinishTaskAsync(int id);
    public Task<bool> DeleteTasksAsync(int[] ids);
    public Task<bool> DeleteTaskAsync(int id);
    public Task<List<TodoItem>> GetAllTasksAsync();
    public Task<List<TodoItem>> SearchTasksAsync(string query);
}
