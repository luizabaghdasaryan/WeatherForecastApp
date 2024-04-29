using BusinessLogic.Exceptions;
using Newtonsoft.Json;

namespace WebApi.Middleware
{
    public class CustomExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger<CustomExceptionHandlingMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");

                httpContext.Response.ContentType = "application/json";

                (httpContext.Response.StatusCode, string message) = ex switch
                {
                    NotFoundException => (StatusCodes.Status404NotFound, ex.Message),
                    _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
                };

                var result = JsonConvert.SerializeObject(new 
                {
                    httpContext.Response.StatusCode,
                    message 
                });

                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}