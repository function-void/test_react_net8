using Microsoft.EntityFrameworkCore;
using UserManagementApp.Application.Features.Users.Queries;
using UserManagementApp.Application.Features.Users.Responses;
using UserManagementApp.Application.Helpers;
using UserManagementApp.Application.Interfaces;
using UserManagementApp.Infrastructure.DataAccess.Context;

namespace UserManagementApp.Infrastructure.DataAccess.Queries;

public class GetUsersQueryHandler(AppDbContext context) : IRequestHandler<GetUsersQuery, PaginatedList<UserDto>>
{
    private readonly AppDbContext _context = context;

    public async Task<PaginatedList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Users
                .Where(x => string.IsNullOrEmpty(request.Search) ||
                            EF.Functions.Like(x.FullName.ToLower(), $"%{request.Search.ToLower()}%") ||
                            EF.Functions.Like(x.Email.ToLower(), $"%{request.Search.ToLower()}%"))
                .Where(x => !x.IsDeleted);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize)
            .Select(x => new UserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                Role = x.Role
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<UserDto>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
