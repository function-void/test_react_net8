using UserManagementApp.Application;
using UserManagementApp.ExceptionHandler;
using UserManagementApp.Infrastructure.DataAccess;
using UserManagementApp.Infrastructure.Services;
using UserManagementApp.Options;
using UserManagementApp.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddConfigureOptions();
builder.Services.AddConfiguredController();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddDataAccessServices(builder.Configuration);

var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "User Management App is running!");

app.Run();
