using System.Linq.Expressions;

namespace UserManagementApp.Domain.Models;

public abstract class Specification<T>(Expression<Func<T, bool>> criteria) where T : class
{
    public Expression<Func<T, bool>> Criteria { get; } = criteria;
    public List<Expression<Func<T, object>>> Include { get; } = [];
    public List<(Expression<Func<T, object>> Include, Expression<Func<object, object>> ThenInclude)> IncludeAndThen { get; } = [];
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    protected void AddInclude(Expression<Func<T, object>> includeExpression) => Include.Add(includeExpression);
    protected void AddIncludeAndThen(Expression<Func<T, object>> includeExpression, Expression<Func<object, object>> thenIncludeExpression) => IncludeAndThen.Add((includeExpression, thenIncludeExpression));
    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;
    protected void AddOrderDescending(Expression<Func<T, object>> orderByDescendingExpression) => OrderByDescending = orderByDescendingExpression;
}
