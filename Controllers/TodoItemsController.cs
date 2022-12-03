using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoItemsController(TodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<List<TodoItem>> Get() =>
            await _todoService.GetAsync();

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _todoService.GetAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateTodoItem(TodoItem newTodoItem)
        {
            await _todoService.CreateAsync(newTodoItem);

            return CreatedAtAction(nameof(Get), new { id = newTodoItem.Id }, newTodoItem);
        }

        // DELETE: api/TodoItems/5
        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItem updatedTodoItem)
        {
            if (id != updatedTodoItem.Id)
            {
                return BadRequest();
            }

            var todoItem = await _todoService.GetAsync(id);

            if (todoItem is null)
            {
                return NotFound();
            }

            updatedTodoItem.Id = todoItem.Id;

            await _todoService.UpdateAsync(id, updatedTodoItem);


            return CreatedAtAction(nameof(Get), new { id = updatedTodoItem.Id }, updatedTodoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoService.GetAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoService.RemoveAsync(id);

            return NoContent();
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                todoItem = new TodoItem
                {
                    Id = todoItem.Id,
                    Name = todoItem.Name,
                    IsComplete = todoItem.IsComplete
                }
            };
    }
}