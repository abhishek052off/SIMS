using Microsoft.AspNetCore.Mvc;

namespace SIMSWeb.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
