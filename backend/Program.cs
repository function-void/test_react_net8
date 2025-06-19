using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;
using UserManagementApp.Application;
using UserManagementApp.ExceptionHandler;
using UserManagementApp.Infrastructure.DataAccess;
using UserManagementApp.Infrastructure.Services;
using UserManagementApp.Options;
using UserManagementApp.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails(
            options =>
            {
                options.CustomizeProblemDetails = context =>
                {
                    Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    context.ProblemDetails.Detail = "No details";
                    context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                    context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                };
            });

builder.Services.AddCors(options =>
{
    options.AddPolicy("dev", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddConfigureOptions();
builder.Services.AddConfiguredController();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddDataAccessServices(builder.Configuration);

var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseCors("dev");
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseRouting();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(option =>
{
    option.EnableFilter();
    option.DisplayRequestDuration();
});

app.MapGet("/", () => "User Management App is running!");

app.Run();
