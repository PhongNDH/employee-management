using System.Linq.Expressions;
using EmployeeManagement.Models.Entity;

namespace EmployeeManagement.Models.Interface.Specification;

public class Specification<T> : ISpecification<T> where T : GenericEntity
{
    protected Specification() //Criteria = null;
    {
        
    }
    protected Specification(Expression<Func<T, bool>> criteria)
    {   
        Criteria = criteria;
    }
    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Expression<Func<T, object>>>? Includes { get; } = new();

    public List<string> IncludeStrings { get; } = new();
    
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set;}
    public int Take { get; private set;}
    public int Skip { get; private set;}
    public bool IsPagingEnabled { get; private set;}

    protected void AddInclude(Expression<Func<T ,object>> includeExpression)
    {
        Includes?.Add(includeExpression);
    }
    
    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
    
    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderBy = orderByDescExpression;
    }
    
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}