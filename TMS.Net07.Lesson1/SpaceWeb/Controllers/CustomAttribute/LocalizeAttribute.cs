using Microsoft.AspNetCore.Mvc.Filters;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers.CustomAttribute
{
    public class LocalizeAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            var userService =
                (UserService)context.HttpContext.RequestServices
                .GetService(typeof(UserService));

            var user = userService.GetCurrent();
            if (user == null)
            {
                // TODO
                
            }

            switch (user.Lang)
            {
                case EfStuff.Model.Lang.En:
                    CultureInfo.DefaultThreadCurrentUICulture
                        = new CultureInfo("en-US");
                    break;
                case EfStuff.Model.Lang.Ru:
                    CultureInfo.DefaultThreadCurrentUICulture
                        = new CultureInfo("ru-RU");
                    break;
                default:
                    break;
            }
        }
    }
}
