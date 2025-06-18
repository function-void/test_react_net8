using UserManagementApp.Application.Features.Users.Specifications;
using UserManagementApp.Application.Interfaces;
using UserManagementApp.Application.Services;
using UserManagementApp.Domain.Models;

namespace UserManagementApp.Application.Features.Users.Commands;

public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IUserService userService, IRoleService roleService) : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserService _userService = userService;
    private readonly IRoleService _roleService = roleService;

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var checkRoleResult = _roleService.CheckRole(request.Role);

        if (checkRoleResult.IsFailure)
            return checkRoleResult.Error!;

        var user = await _userService.GetAsync(new GetUserByIdSpecification(request.Id), cancellationToken);

        if (user is null)
            return Error.NotFound;

        user.Email = request.Email;
        user.FullName = request.FullName;
        user.Role = request.Role;

        _userService.Update(user);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result ? Result.Success() : Error.BadRequest;
    }
}