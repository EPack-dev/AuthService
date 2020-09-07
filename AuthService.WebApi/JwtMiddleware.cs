using System.Linq;
using System.Threading.Tasks;
using AuthService.Model;
using Microsoft.AspNetCore.Http;

namespace AuthService.WebApi
{
    public class JwtMiddleware
    {
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")?.Last();
            await AssignUser(token, context, userService);
            await _next(context);
        }

        private async Task AssignUser(string? token, HttpContext context, IUserService userService)
        {
            if (token is null)
            {
                return;
            }

            User? user = await userService.GetByToken(token);
            if (user is null)
            {
                return;
            }

            context.Items["User"] = user;
        }

        private readonly RequestDelegate _next;
    }
}
