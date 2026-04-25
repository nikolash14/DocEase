using DocEase.Api.Common;
using Serilog.Context;

namespace DocEase.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                LogContext.PushProperty("Controller", context.Request.RouteValues["controller"]);
                LogContext.PushProperty("Action", context.Request.RouteValues["action"]);
                LogContext.PushProperty("UserId", context.User.Identity?.Name ?? "Anonymous");
                await _next(context);
            }
            catch (Exception ex)
            {
                ServerResponse(context, ex);
            }
        }

        private static void ServerResponse(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            var response = ApiResponse<object>.Fail(
                message: $"Internal Server Error: {ex.Message}",
                statusCode: 500
            );
            context.Response.WriteAsJsonAsync(response);
        }

    }
}
