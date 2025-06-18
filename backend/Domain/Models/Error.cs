using System.Net;

namespace UserManagementApp.Domain.Models;

public sealed class Error
{
    public string Title { get; init; }
    public int Status { get; init; }
    public string Detail { get; init; }
    public string Instance { get; init; }

    public Error(HttpStatusCode code, string message)
    {
        Title = GetTitle(code);
        Status = (int)code;
        Detail = message;
        Instance = GetRFC(code);
    }

    public Error(string message)
    {
        Title = GetTitle(HttpStatusCode.BadRequest);
        Status = (int)HttpStatusCode.BadRequest;
        Detail = message;
        Instance = GetRFC(HttpStatusCode.BadRequest);
    }

    public static readonly Error BadRequest = new(HttpStatusCode.BadRequest, "The server could not understand the request due to invalid syntax. Please check your input and try again.");
    public static readonly Error Forbidden = new(HttpStatusCode.Forbidden, "You do not have permission to access this resource. Please contact support if you believe this is an error.");
    public static readonly Error NotFound = new(HttpStatusCode.NotFound, "The requested resource could not be found. Please check the URL or contact support for assistance.");
    public static readonly Error RequestTimeout = new(HttpStatusCode.RequestTimeout, "The server took too long to respond. Please try again later.");
    public static readonly Error Conflict = new(HttpStatusCode.Conflict, "There was a conflict with the current state of the resource. Please try again or contact support.");
    public static readonly Error InternalServerError = new(HttpStatusCode.InternalServerError, "The server encountered an unexpected error. Please try again later or contact support.");
    public static readonly Error ServiceUnavailable = new(HttpStatusCode.ServiceUnavailable, "The server is currently unavailable due to maintenance or overload. Please try again later.");

    private static string GetRFC(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.BadRequest => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            HttpStatusCode.Forbidden => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            HttpStatusCode.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            HttpStatusCode.RequestTimeout => "https://tools.ietf.org/html/rfc7231#section-6.5.7",
            HttpStatusCode.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            HttpStatusCode.InternalServerError => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            HttpStatusCode.ServiceUnavailable => "https://tools.ietf.org/html/rfc7231#section-6.6.4",
            _ => throw new NotImplementedException(),
        };
    }

    private static string GetTitle(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.BadRequest => nameof(HttpStatusCode.BadRequest),
            HttpStatusCode.Forbidden => nameof(HttpStatusCode.Forbidden),
            HttpStatusCode.NotFound => nameof(HttpStatusCode.NotFound),
            HttpStatusCode.RequestTimeout => nameof(HttpStatusCode.RequestTimeout),
            HttpStatusCode.Conflict => nameof(HttpStatusCode.Conflict),
            HttpStatusCode.InternalServerError => nameof(HttpStatusCode.InternalServerError),
            HttpStatusCode.ServiceUnavailable => nameof(HttpStatusCode.ServiceUnavailable),
            _ => throw new NotImplementedException(),
        };
    }
}
