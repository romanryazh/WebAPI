namespace WebAPI.Middlewares;

public class AuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Query["token"];
        if (string.IsNullOrEmpty(token)) 
            context.Response.StatusCode = 401;
        else
        {
            if (token == "123")
                context.Response.StatusCode = 419;
            else
                await next(context);
        }
    }
}