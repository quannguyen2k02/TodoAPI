using BusinessLogicLayer;
using DataAccessLayer.Enitites;
using Microsoft.AspNetCore.Mvc;

namespace Task.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{    
    private readonly ITodoService _service;

    public TaskController(ITodoService service) {
        _service = service;
    }
    [HttpPost]
    public async Task<IActionResult> AddTask(TodoItem item)
    {
        if (ModelState.IsValid)
        {
            var r = await _service.AddTaskAsync(item);
            return Ok(r);
        }
        return BadRequest();
    }
    [HttpGet]
    public async Task<IActionResult> GetAllTask()
    {
        var list = await _service.GetAllTaskAsync();
        return Ok(list);    
    }
    [HttpPut]
    public async Task<IActionResult> FinishTasks(int[] ids)
    {
        var result = await _service.FinishTasksAsync(ids);
        if (result)
            return Ok(ids);
        return BadRequest();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> FinishTask(int id)
    {
        var result = await _service.FinishTaskAsync(id);
        if (result)
            return Ok(id);
        return BadRequest();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result =await _service.DeleteTaskAsync(id);
        if (result)
        {
            return Ok(result);
        }
        return BadRequest();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteTasks(int[] ids)
    {
        var result = await _service.DeleteTasksAsync(ids);
        if(result)
            return Ok(result);
        return BadRequest();
    }
    [HttpGet("{query}")]
    public async Task<IActionResult> SearchTasks(string query)
    {
        var result = await _service.SearchTasksAsync(query);
        return Ok(result);
    }
}
