using Microsoft.AspNetCore.Mvc;

namespace SIMSWeb.Controllers
{
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
