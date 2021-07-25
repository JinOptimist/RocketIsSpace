using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SpaceWeb.Service;

namespace SpaceWeb.Controllers.CustomAttribute
{
    public class IsEmployeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _userService = (IUserService)context
                .HttpContext
                .RequestServices
                .GetService(typeof(IUserService));
            if (!_userService.IsEmploye())
            {
                context.Result = new ForbidResult();
            }
            base.OnActionExecuting(context);
        }

    }
}