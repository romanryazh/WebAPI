namespace WebAPI.Middlewares;

public class CharsetMiddleware(RequestDelegate next, string charset = "utf-8")
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            if (ShouldAddCharset(context))
            {
                context.Response.ContentType = 
                    $"{context.Response.ContentType}; charset={charset}";
            }
            return Task.CompletedTask;
        });

        await next(context);
    }

    private static bool ShouldAddCharset(HttpContext context)
    {
        return context.Response.ContentType?.StartsWith("text/") == true
               && !context.Response.ContentType.Contains("charset=");
    }
}