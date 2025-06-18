using FluentValidation;
using UserManagementApp.Application.Features.Users.Responses;
using UserManagementApp.Application.Helpers;
using UserManagementApp.Application.Interfaces;

namespace UserManagementApp.Application.Features.Users.Queries;

public class GetUsersQuery : PaginatedQuery, IRequest<PaginatedList<UserDto>>
{
    public string? Search { get; init; }
}

public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .NotEmpty()
            .GreaterThan(0);
    }
}