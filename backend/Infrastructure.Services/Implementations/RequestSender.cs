using FluentValidation;
using UserManagementApp.Application.Interfaces;
using UserManagementApp.Application.Services;

namespace UserManagementApp.Infrastructure.Services.Implementations;

public class RequestSender(IServiceProvider serviceProvider, ILogger<RequestSender> logger, ICurrentUserService currentUserService) : IRequestSender
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<RequestSender> _logger = logger;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<TResponse> ExecuteAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        var requestName = request.GetType().Name;

        _logger.LogInformation("Request {RequestName} is called by {UserId}. CorrelationId: {CorrelationId}. Request parameters: {Request}.",
            requestName, _currentUserService.UserId, _currentUserService.CorrelationId, request);

        await using var scope = _serviceProvider.CreateAsyncScope();
        var validators = scope.ServiceProvider.GetServices<IValidator<TRequest>>();

        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(request, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var response = await handler.Handle(request, cancellationToken);

        _logger.LogInformation("Request {RequestName} is completed", requestName);

        return response;
    }
}
