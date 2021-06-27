using System;
using System.Text.Json;
using System.Threading.Tasks;
using AuthService.Model;
using AuthService.WebApp.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AuthService.WebApp
{
    public class ExceptionMiddleware
    {
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            var error = new ApiErrorDto();
            switch (exception)
            {
                case ServiceException ex:
                    context.Response.StatusCode = (int)ex.StatusCode;
                    error.Message = ex.Message;
                    break;

                default:
                    _logger.LogError(exception.ToString());
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    error.Message = "Unknown internal error.";
                    break;
            }

            string responseBody = JsonSerializer.Serialize(error);
            await context.Response.WriteAsync(responseBody);
        }

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
    }
}
