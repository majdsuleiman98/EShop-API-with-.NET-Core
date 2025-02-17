
using API.Errors;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware(IHostEnvironment env, RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {                       
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    await HandleNotFoundAsync(context);
                }
                await next(context);
            }          
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = env.IsDevelopment()
                ? new ApiErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : new ApiErrorResponse(context.Response.StatusCode, ex.Message, "Internal server error");
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            return context.Response.WriteAsync(json);
        }

        private static Task HandleNotFoundAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ApiErrorResponse(
                StatusCodes.Status404NotFound,
                "This endpoint is not found",
                null
            );
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            return context.Response.WriteAsync(json);
        }        
    }
}
