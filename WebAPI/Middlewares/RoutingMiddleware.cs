namespace WebAPI.Middlewares;

public class RoutingMiddleware(RequestDelegate next, IWebHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();
        switch (path)
        {
            case "/":
                await SendHtmlFileAsync(context, "index.html");
                break;
            case "/data":
                await SendHtmlFileAsync(context, "data.html");
                break;
            case "/tail":
                await SendImageFileAsync(context, "tail.jpg");
                break;
            default:
                await next(context);
                break;
        }
    }

    private async Task SendHtmlFileAsync(HttpContext context, string fileName)
    {
        context.Response.ContentType = "text/html";
        var filePath = Path.Combine(env.WebRootPath, fileName);
        await context.Response.SendFileAsync(filePath);
    }

    private async Task SendImageFileAsync(HttpContext context, string fileName)
    {
        var filePath = Path.Combine(env.WebRootPath, fileName);
        var contentType = GetImageContentType(filePath);
        context.Response.ContentType = contentType;
        await context.Response.SendFileAsync(filePath);
    }

    private static string GetImageContentType(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLower();

        return extension switch
        {
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            "webp" => "image/webp",
            _ => "application/octet-stream"
        };
    }
}