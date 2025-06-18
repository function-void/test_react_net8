using UserManagementApp.Application.Interfaces;

namespace UserManagementApp.Application.Services;

public interface IRequestSender
{
    Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResult>;
}
