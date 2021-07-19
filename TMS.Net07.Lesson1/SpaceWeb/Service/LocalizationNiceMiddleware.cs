using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpaceWeb.Service
{
    public class LocalizationNiceMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationNiceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userService =
                (IUserService)context.RequestServices
                .GetService(typeof(IUserService));

            var user = userService.GetCurrent();
            if (user == null)
            {
                var langKey = context.Request.Cookies["guestLang"] as string;
                switch (langKey)
                {
                    case "En":
                        CultureInfo.DefaultThreadCurrentUICulture
                            = new CultureInfo("en-US");
                        break;
                    case "Ru":
                        CultureInfo.DefaultThreadCurrentUICulture
                            = new CultureInfo("ru-RU");
                        break;
                    default:
                        break;
                }
            }
            else
            {
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

            await _next(context);
        }
    }
}
