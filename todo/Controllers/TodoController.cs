using BusinessLogicLayer;
using DataAccessLayer.Enitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service) {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> AddTodo(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                var r = await _service.AddTodo(item);
                return Ok(r);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAll();
            return Ok(list);    
        }
        [HttpPost("FinishTodo")]
        public async Task<IActionResult> FinishTodo(int id)
        {
            var result = await _service.FinishTodo(id);
            if (result)
                return Ok(id);
            return BadRequest();
        }
        [HttpDelete("DeleteTodoFinished")]
        public async Task<IActionResult> DeleteTodoFinished()
        {
            var result =await _service.DeleteTodoFinished();
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("DeleteTodo")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var result = await _service.DeleteTodo(id);
            if(result)
                return Ok(result);
            return BadRequest();
        }
    }
}
