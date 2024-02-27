using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Specification;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DataAccess.Specification;

public static class SpecificationEvaluator<T> where T : GenericEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderBy(spec.OrderByDescending);
        }

        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        if (spec.Includes is { Count: > 0 })
        {
            query = spec.Includes.Aggregate(query, (current, include) =>
                current.Include(include));
        }

        if (spec.IncludeStrings is { Count: > 0 })
        {
            query = spec.IncludeStrings.Aggregate(query, (current, include) =>
                current.Include(include));
        }


        return query;
    }
}