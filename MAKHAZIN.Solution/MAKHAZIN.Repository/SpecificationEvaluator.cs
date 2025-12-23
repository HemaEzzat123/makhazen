using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Sepecification;
using Microsoft.EntityFrameworkCore;

namespace MAKHAZIN.Repository
{
    internal static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            // Include related entities (expression-based)
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            // Include related entities (string-based for nested includes like "Bids.User")
            query = spec.IncludeStrings.Aggregate(query, (currentQuery, includeString) => currentQuery.Include(includeString));

            // Apply pagination
            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            return query;
        }
    }
}

