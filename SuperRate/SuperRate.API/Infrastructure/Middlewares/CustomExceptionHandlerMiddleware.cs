using System.Net;
using Newtonsoft.Json;
using SuperRate.API.Infrastructure.Classes;

namespace SuperRate.API.Infrastructure.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        ErrorLog error = new(httpContext, ex);

        var log = JsonConvert.SerializeObject(error);

        httpContext.Response.Clear();
        httpContext.Response.ContentType = "application/json";

        if (error.Status.HasValue)
            httpContext.Response.StatusCode = error.Status.Value;
        else
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await httpContext.Response.WriteAsync(log);
    }
}