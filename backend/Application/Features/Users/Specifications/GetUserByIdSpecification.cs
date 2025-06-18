using UserManagementApp.Domain.Models;

namespace UserManagementApp.Application.Features.Users.Specifications;

public class GetUserByIdSpecification : Specification<User>
{
    public GetUserByIdSpecification(Guid id) : base(user => user.Id == id && !user.IsDeleted) { }
}
