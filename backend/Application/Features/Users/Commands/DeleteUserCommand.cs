using FluentValidation;
using UserManagementApp.Application.Interfaces;
using UserManagementApp.Domain.Models;

namespace UserManagementApp.Application.Features.Users.Commands;

public record class DeleteUserCommand(Guid Id) : IRequest<Result>;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
