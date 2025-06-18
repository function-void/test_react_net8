using UserManagementApp.Application.Interfaces;
using UserManagementApp.Application.Services;
using UserManagementApp.Domain.Models;

namespace UserManagementApp.Application.Features.Users.Commands;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserService userService, IRoleService roleService) : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserService _userService = userService;
    private readonly IRoleService _roleService = roleService;

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var checkRoleResult = _roleService.CheckRole(request.Role);

        if (checkRoleResult.IsFailure)
            return checkRoleResult.Error!;

        var user = new User()
        {
            FullName = request.FullName,
            Email = request.Email,
            Role = request.Role,
        };

        await _userService.CreateAsync(user, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result ? user.Id : Error.BadRequest;
    }
}