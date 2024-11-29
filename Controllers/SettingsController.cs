using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ToDoListApp.Controllers
{
        public class SettingsController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }
        }

}
