using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpaceWeb.Service
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var logger = context.RequestServices
                    .GetService(typeof(ILogger<ErrorHandlerMiddleware>)) as ILogger<ErrorHandlerMiddleware>;
                logger.LogError($"Path: {context.Request.Path} Error:{error.Message}");
            }
        }
    }
}
