using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TechPrimeLab.Middleware
{
    public class CustomExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(ILogger<CustomExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An unhandled exception occurred.");

                // Set the response status code to 500 (Internal Server Error)
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Optionally, you can customize the response content
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Internal Server Error");

                // Alternatively, you can return a JSON response with more details:
                // context.Response.ContentType = "application/json";
                // await context.Response.WriteAsync($"{{\"error\": \"{ex.Message}\"}}");
            }
        }
    }
}
