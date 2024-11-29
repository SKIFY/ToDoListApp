using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoListApp.Models; // Не забудьте додати простір імен для моделей, якщо Task знаходиться в папці Models

namespace ToDoListApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Додаємо DbSet для моделі Task
        public DbSet<ToDoTasks> ToDoTasks { get; set; }  // В контексте данных
    }
}
