using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMSWeb.Business.IService;
using SIMSWeb.Business.Service;
using SIMSWeb.Model.Models;
using SIMSWeb.Models;

namespace SIMSWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly UserSession _session;

        public HomeController(ILogger<HomeController> logger, IUserService userService, UserSession session)
        {
            _logger = logger;
            _userService = userService;
            _session = session;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("My-Profile")]
        public async Task<ActionResult> Profile()
        {
            if (!string.IsNullOrEmpty(_session.Email)) {
                var user = await _userService.GetUserByEmail(_session.Email);
                return View(user);
            }
            return View(new User());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
