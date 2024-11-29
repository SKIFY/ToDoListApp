using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoListApp.Controllers
{
    [Authorize]
    public class TasksViewController : Controller
    {
        // GET: /Tasks/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}