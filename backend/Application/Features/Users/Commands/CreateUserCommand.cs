using FluentValidation;
using UserManagementApp.Application.Interfaces;
using UserManagementApp.Domain.Models;

namespace UserManagementApp.Application.Features.Users.Commands;

public record class CreateUserCommand(string FullName, string Email, string Role) : IRequest<Result<Guid>>;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(256)
            .EmailAddress();

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(512);

        RuleFor(x => x.Role)
            .NotEmpty()
            .MaximumLength(256);
    }
}