using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Management.Domain.Settings;
using Microsoft.AspNetCore.Http;

namespace Management.API.Infrastructure.Middlewares
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;

        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            var user = httpContext.User;
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            int userId = 0;
            Int32.TryParse(id, out userId);


            if (userId > 0)
            {
                CurrentUser.Id = userId;
                CurrentUser.Email = user.FindFirst(ClaimTypes.Email)?.Value;
                CurrentUser.Name = user.FindFirst(ClaimTypes.Name)?.Value;
                CurrentUser.Surname = user.FindFirst(ClaimTypes.Surname)?.Value;
                CurrentUser.Jti = user.FindFirst("jti")?.Value;
            }

            await _next(httpContext);
        }
    }
}
