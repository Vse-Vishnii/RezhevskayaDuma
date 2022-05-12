using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Role[] roles;

        public AuthorizeAttribute()
        {

        }

        public AuthorizeAttribute(Role[] roles)
        {
            this.roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
            if (roles != null && !roles.Contains(user.Role))
                context.Result = new JsonResult(new { message = "No permission" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
