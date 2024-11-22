using DataAccessLayer.Enitites;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer;

public class TodoService : ITodoService
{
    private readonly ITodoRepositories _todoRepo;

    public TodoService(ITodoRepositories todoRepo) {
        _todoRepo = todoRepo;
    }
    public async Task<TodoItem> AddTaskAsync(TodoItem item)
    {
        return await _todoRepo.AddTaskAsync(item);
    }

    public async Task<bool> DeleteTasksAsync(int[] ids)
    {
        return await _todoRepo.DeleteTasksAsync(ids);
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        bool result = await _todoRepo.DeleteTaskAsync(id);
        return result;
    }


    public async Task<List<TodoItem>> GetAllTaskAsync()
    {
        var list =await _todoRepo.GetAllTasksAsync();
        return list;
    }

    public async Task<bool> FinishTasksAsync(int[] ids)
    {
        var result = await _todoRepo.FinishTasksAsync(ids);
        return result;
    }
    public async Task<bool> FinishTaskAsync(int id)
    {
        var result = await _todoRepo.FinishTaskAsync(id);
        return result;
    }

    public async Task<List<TodoItem>> SearchTasksAsync(string query)
    {
        var list = await _todoRepo.SearchTasksAsync(query);
        return list;
    }
}
