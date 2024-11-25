using BusinessLogicLayer;
using DataAccessLayer.Enitites;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
            var task = await _service.AddTaskAsync(item);
            return Ok(task);
        }
        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTask()
    {
        var listTask = await _service.GetAllTaskAsync();
        return Ok(listTask);    
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeStatusFinish(int id)
    {
        var result = await _service.ChangeTaskFinishAsync(id);
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

    [HttpGet("Search")]
    public async Task<IActionResult> SearchTasks([FromQuery]string ?query,[FromQuery] string ?status = null)
    {
        string querySandardization = NormalizeSpaces(query);
        var result = await _service.SearchTasksAsync(querySandardization, status);
        return Ok(result);
    }

    [HttpGet("DoingTasks")]
    public async Task<IActionResult> GetDoingTasks()
    {
        var doingTasks = await _service.GetDoingTasksAsync();
        return Ok(doingTasks);
    }

    [HttpGet("FinishedTasks")]
    public async Task<IActionResult> GetFinishedTasks()
    {
        var finishedTasks = await _service.GetFinishedTasksAsync();
        return Ok(finishedTasks);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(TodoItem item)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.UpdateTaskAsync(item);
            return Ok(result);
        }
        return BadRequest();
    }

    public static string NormalizeSpaces(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        // Loại bỏ các dấu cách thừa đầu và cuối chuỗi
        input = input.Trim();

        // Thay thế các dấu cách liên tiếp thành một dấu cách duy nhất
        input = Regex.Replace(input, @"\s+", " ");

        return input;
    }
}
