using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SIMSWeb.Model.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SIMSWeb.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserSessionMiddleware
    {
        private readonly RequestDelegate _next;

        public UserSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, UserSession session)
        {
            var contextUser = httpContext?.User;
            if (contextUser != null && contextUser.Identities.Any())
            {
                session.Id = Convert.ToInt32(contextUser.FindFirstValue(ClaimTypes.NameIdentifier));
                session.Email = contextUser.FindFirstValue(ClaimTypes.Email);
                session.Name = contextUser.FindFirstValue(ClaimTypes.Name);
                session.Role = contextUser.FindFirstValue(ClaimTypes.Role);
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    //public static class UserSessionMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseUserSessionMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<UserSessionMiddleware>();
    //    }
    //}
}


