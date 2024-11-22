using DataAccessLayer.Enitites;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepo) {
        _todoRepository = todoRepo;
    }
    public async Task<TodoItem> AddTaskAsync(TodoItem item)
    {
        return await _todoRepository.AddTaskAsync(item);
    }

    public async Task<bool> DeleteTasksAsync(int[] ids)
    {
        return await _todoRepository.DeleteTasksAsync(ids);
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        return await _todoRepository.DeleteTaskAsync(id);
    }


    public async Task<List<TodoItem>> GetAllTaskAsync()
    {
        return await _todoRepository.GetAllTasksAsync();
    }

    public async Task<bool> FinishTasksAsync(int[] ids)
    {
        return await _todoRepository.FinishTasksAsync(ids);

    }
    public async Task<bool> FinishTaskAsync(int id)
    {
        return await _todoRepository.FinishTaskAsync(id);
    }

    public async Task<List<TodoItem>> SearchTasksAsync(string query)
    {
        return await _todoRepository.SearchTasksAsync(query);
    }

    public async Task<bool> ChangeTaskFinishAsync(int id)
    {
        return  await _todoRepository.ChangeStatusFinishAsync(id);
     
    }

    public async Task<List<TodoItem>> GetFinishedTasksAsync()
    {
        return await _todoRepository.GetFinishedTasksAsync();
    }

    public async Task<List<TodoItem>> GetDoingTasksAsync()
    {
        return await _todoRepository.GetDoingTasksAsync();
    }
}
