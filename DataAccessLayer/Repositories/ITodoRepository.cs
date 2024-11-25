using DataAccessLayer.Enitites;

namespace DataAccessLayer.Repositories;

public interface ITodoRepository
{
    public Task<TodoItem> AddTaskAsync(TodoItem item);
    public Task<bool> FinishTasksAsync(int[] ids);
    public Task<bool> FinishTaskAsync(int id);
    public Task<bool> DeleteTasksAsync(int[] ids);
    public Task<bool> DeleteTaskAsync(int id);
    public Task<List<TodoItem>> GetAllTasksAsync();
    public Task<List<TodoItem>> SearchTasksAsync(string ?query, string ?status);
    public Task<bool> ChangeStatusFinishAsync(int id);
    public Task<List<TodoItem>> GetFinishedTasksAsync();
    public Task<List<TodoItem>> GetDoingTasksAsync();
    public Task<bool> UpdateTaskAsync(TodoItem item);
}
