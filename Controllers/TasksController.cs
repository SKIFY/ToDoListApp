using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TasksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var userId = _userManager.GetUserId(User);
            var tasks = await _context.ToDoTasks  // Используем модель ToDoTasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] ToDoTasks task)  // Используем модель ToDoTasks
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User is not authenticated.");
            }

            task.UserId = userId;
            task.IsCompleted = false;
            task.CompletedAt = null;

            _context.ToDoTasks.Add(task);  // Используем модель ToDoTasks

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var userId = _userManager.GetUserId(User);
            var task = await _context.ToDoTasks  // Используем модель ToDoTasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] ToDoTasks updatedTask)  // Используем модель ToDoTasks
        {
            if (updatedTask == null)
            {
                return BadRequest("Task data is missing.");
            }

            var userId = _userManager.GetUserId(User);
            var task = await _context.ToDoTasks  // Используем модель ToDoTasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null)
            {
                return NotFound();
            }

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.DueDate = updatedTask.DueDate;
            task.DueTime = updatedTask.DueTime;
            task.IsCompleted = updatedTask.IsCompleted;
            task.CompletedAt = updatedTask.CompletedAt;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var userId = _userManager.GetUserId(User);
            var task = await _context.ToDoTasks  // Используем модель ToDoTasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null)
            {
                return NotFound();
            }

            task.IsCompleted = true;
            task.CompletedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = _userManager.GetUserId(User);
            var task = await _context.ToDoTasks  // Используем модель ToDoTasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null)
            {
                return NotFound();
            }

            _context.ToDoTasks.Remove(task);  // Используем модель ToDoTasks
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
