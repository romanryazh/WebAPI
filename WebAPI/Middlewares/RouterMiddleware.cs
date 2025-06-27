namespace WebAPI.Middlewares;

public class RouterMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path;
        if (path == "/")
        {
            await context.Response.WriteAsync("Hello");
        }
        else
        {
            if (path == "/admin") 
                await context.Response.WriteAsync("Admin Panel");
            else
                context.Response.StatusCode = 404;
        }
    }
}