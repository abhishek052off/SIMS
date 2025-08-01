using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SIMSWeb.Business.IService;
using SIMSWeb.Model.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SIMSWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public LoginController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profile", "Home");
            }
            return View();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(string email, string password)
        {
            var user = await _userService.AuthenticateUser(email, password);

            if (user == null)
            {
                ModelState.AddModelError("invalid_login", "User not found");
            }
            else
            {
                //var token = GenerateToken(user);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", principal);

                return RedirectToAction("Profile", "Home");

            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Index", "Login");
        }

       /* private string GenerateToken(User user)
        {
            var jwtConfig = _configuration.GetSection("JWTSettings");

            var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]);
            var issuer = jwtConfig["Issuer"];
            var audience = jwtConfig["Audience"];
            var expiration = jwtConfig["ExpiryMinutes"];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
           {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(expiration)),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
               new SymmetricSecurityKey(key),
               SecurityAlgorithms.HmacSha256Signature
           )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
       */
    }
}
