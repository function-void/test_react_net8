using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace UserManagementApp.ExceptionHandler;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment environment) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;
    private readonly bool _includeDetails = !environment.IsProduction();

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        HttpResponse response = httpContext.Response;
        response.ContentType = "application/problem+json";
        var problemDetails = new ProblemDetails();
        var problemDetailsInstance = $"{httpContext.Request.Method} {httpContext.Request.Path}";

        switch (exception)
        {
            case ValidationException or ArgumentException or ArgumentNullException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = nameof(HttpStatusCode.BadRequest);
                problemDetails.Detail = exception.Message;
                problemDetails.Instance = problemDetailsInstance;
                if (exception is ValidationException validationException)
                    problemDetails.Extensions["errors"] = validationException.Errors.Select(x => new { x.ErrorMessage, x.PropertyName, x.ErrorCode });
                break;
            case KeyNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
                problemDetails.Status = (int)HttpStatusCode.NotFound;
                problemDetails.Title = nameof(HttpStatusCode.NotFound);
                problemDetails.Detail = exception.Message;
                problemDetails.Instance = problemDetailsInstance;
                break;
            case TimeoutException:
                response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.7";
                problemDetails.Status = (int)HttpStatusCode.RequestTimeout;
                problemDetails.Title = nameof(HttpStatusCode.RequestTimeout);
                problemDetails.Detail = exception.Message;
                problemDetails.Instance = problemDetailsInstance;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                problemDetails.Title = nameof(HttpStatusCode.InternalServerError);
                problemDetails.Detail = exception.Message;
                problemDetails.Instance = problemDetailsInstance;
                break;
        }

        if (_includeDetails)
            problemDetails.Detail = exception.ToString();

        var traceId = Activity.Current?.Id;
        var httpTraceId = httpContext?.TraceIdentifier;

        if (traceId != null)
            problemDetails.Extensions["traceId"] = traceId;

        if (httpTraceId != null)
            problemDetails.Extensions["requestId"] = httpTraceId;

        _logger.LogError("An unhandled exception has occurred: {Message}. Handled by: {ProblemDetails}", exception.Message, problemDetails);

        await response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
