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
                string lang = null;
                var langCookie = context.HttpContext.Request.Cookies["Lang"];
                if (langCookie != null)
                {
                    lang = langCookie.;
                }
                else
                {
                    var userLanguage = Request.UserLanguages;
                    var userLang = userLanguage != null ? userLanguage[0] : "";
                    if (userLang != "")
                    {
                        lang = userLang;
                    }
                    else
                    {
                        lang = LanguageMang.GetDefaultLanguage();
                    }
                }
                new LanguageMang().SetLanguage(lang);
                return base.BeginExecuteCore(callback, state);
                return;
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
