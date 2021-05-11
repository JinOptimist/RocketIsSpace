using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.Service;

namespace SpaceWeb.Controllers.CustomAttribute
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public IsAdminAttribute()
        {
        }
        public override void OnActionExecuting(
            ActionExecutingContext context)
        {
            var _userService = (IUserService)context
                .HttpContext
                .RequestServices
                .GetService(typeof(IUserService));
            if (!_userService.IsAdmin())
            {
                context.Result = new ForbidResult();
            }
        }
    }
}