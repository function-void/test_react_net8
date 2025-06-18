using UserManagementApp.Application.Features.Users.Specifications;
using UserManagementApp.Application.Interfaces;
using UserManagementApp.Application.Services;
using UserManagementApp.Domain.Models;

namespace UserManagementApp.Application.Features.Users.Commands;

public class DeleteUserCommandHandler(IUnitOfWork unitOfWork, IUserService userService) : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserService _userService = userService;

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetAsync(new GetUserByIdSpecification(request.Id), cancellationToken);

        if (user is null)
            return Error.NotFound;

        _userService.Remove(user);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result ? Result.Success() : Error.BadRequest;
    }
}