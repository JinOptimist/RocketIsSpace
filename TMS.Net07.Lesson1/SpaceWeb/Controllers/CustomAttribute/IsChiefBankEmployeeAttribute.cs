using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers.CustomAttribute
{
    public class IsChiefBankEmployeeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(
            ActionExecutingContext context)
        {
            var _userService = (IUserService)context
                .HttpContext
                .RequestServices
                .GetService(typeof(IUserService));
            if (!_userService.IsChiefBankEmployee())
            {
                context.Result = new ForbidResult();
            }


            base.OnActionExecuting(context);
        }
    }
}
