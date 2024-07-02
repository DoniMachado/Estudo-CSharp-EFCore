using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace EFCore.WebAPI.Common.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ExceptionResponse(context.Response.StatusCode, ex.Message,"Internal Server Error");
            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        
        }

        public class ExceptionResponse
        {
            public int StatusCode { get; set; } 
            public string Message { get; set; } 
            public string Details { get; set; }
            
            public ExceptionResponse(int statusCode, string message, string details = null)
            {
                StatusCode = statusCode;
                Message = message;
                Details = details;
            }
        }
    }
}
