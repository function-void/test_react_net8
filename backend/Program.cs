var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "User Management App is running!");

app.Run();
