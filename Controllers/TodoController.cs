using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEAApi.Data;
using PEAApi.Models;

namespace PEAApi.Controllers
{   
    [Route("[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        //Setup ตัว Database Context เพื่อรับ-เขียนข้อมูล Database ได้
        private readonly TodoDb _context;
        public TodoController(TodoDb context)
        {
            _context = context;
        }

        //GET api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            if (_context.Todos == null)
            {
                return NotFound();
            }

            return await _context.Todos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            if (_context.Todos == null)
            {
                return NotFound();
            }

            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return todo;
        }

        //POST api/Todo
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            if (_context.Todos == null)
            {
                return Problem("Entity set 'TodoContext.Todo'  is null.");
            }
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            if (_context.Todos == null)
            {
                return NotFound();
            }
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return (_context.Todos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}