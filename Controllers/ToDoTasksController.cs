using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoTasksApi.Data;
using ToDoTasksApi.Models;

namespace ToDoTasksApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoTasksController : ControllerBase
{
    private readonly ToDoContext _context;

    public ToDoTasksController(ToDoContext context)
    {
        _context = context;
    }

    // GET api/ToDoTasks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoTask>>> GetTasks()
    {
        return await _context.ToDoTasks.ToListAsync();
    }

    // get a specific task by id
    // GET api/ToDoTasks/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoTask>> GetToDoTask(int id)
    {
        var toDoTask = await _context.ToDoTasks.FindAsync(id);

        if (toDoTask == null)
        {
            return NotFound();
        }

        return toDoTask;
    }

    // create a new task
    // POST api/ToDoTasks
    [HttpPost]
    public async Task<ActionResult<ToDoTask>> PostToDoTask(ToDoTask toDoTask)
    {
        _context.ToDoTasks.Add(toDoTask);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetToDoTask", new { id = toDoTask.Id }, toDoTask);
    }

    // Update task by id
    // PUT api/ToDoTasks/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutToDoTask(int id, ToDoTask toDoTask)
    {
        if (id != toDoTask.Id)
        {
            return BadRequest();
        }

        _context.ToDoTasks.Update(toDoTask);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ToDoTaskExists(id))
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

    // Delete a task by id
    // DELETE api/ToDoTasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToDoTask(int id)
    {
        var toDoTask = await _context.ToDoTasks.FindAsync(id);
        if (toDoTask == null)
        {
            return NotFound();
        }

        _context.ToDoTasks.Remove(toDoTask);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ToDoTaskExists(int id)
    {
        return _context.ToDoTasks.Any(e => e.Id == id);
    }
}
