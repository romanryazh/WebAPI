using WebAPI;
using WebAPI.Middlewares;
using RouterMiddleware = WebAPI.Middlewares.RouterMiddleware;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<ErrorMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseMiddleware<RouterMiddleware>();

app.UseMiddleware<CharsetMiddleware>();
app.UseMiddleware<RoutingMiddleware>();

app.Run();