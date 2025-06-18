using Microsoft.EntityFrameworkCore;
using UserManagementApp.Domain.Models;

namespace UserManagementApp.Infrastructure.DataAccess.Implementations;

public static class SpecificationQueryBuilder
{
    public static IQueryable<T> BuildQuery<T>(IQueryable<T> source, Specification<T> specification) where T : class
    {
        var query = source;

        if (specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        query = specification.Include
            .Aggregate(query, (current, includeSpec) => current.Include(includeSpec));

        query = specification.IncludeAndThen
            .Aggregate(query, (current, includeAndThenSpec) => current.Include(includeAndThenSpec.Include).ThenInclude(includeAndThenSpec.ThenInclude));

        if (specification.OrderBy is not null)
            query = query.OrderBy(specification.OrderBy);

        if (specification.OrderByDescending is not null)
            query = query.OrderByDescending(specification.OrderByDescending);

        return query;
    }
}
