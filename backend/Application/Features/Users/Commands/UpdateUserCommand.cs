using FluentValidation;
using UserManagementApp.Application.Interfaces;
using UserManagementApp.Domain.Models;

namespace UserManagementApp.Application.Features.Users.Commands;

public record class UpdateUserCommand(Guid Id, string FullName, string Email, string Role) : IRequest<Result>;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(256)
            .EmailAddress();

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(512);

        RuleFor(x => x.Role)
            .NotEmpty()
            .MaximumLength(256)
            .Must(x => x == "Admin" || x == "Manager" || x == "User");
    }
}
