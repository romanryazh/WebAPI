namespace WebAPI.Middlewares;

public class ErrorMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        switch (context.Response.StatusCode)
        {
            case 401:
            {
                await context.Response.WriteAsync("Unauthorized");
                break;
            }
            case 404:
            {
                await context.Response.WriteAsync("Not Found");
                break;
            }
            case 419:
            {
                await context.Response.WriteAsync("Authentication Timeout");
                break;
            }
            default:
            {
                await context.Response.WriteAsync($"\r \n Token {context.Request.Query["token"]} is valid");
                break;
            }
                
        }
    }
}