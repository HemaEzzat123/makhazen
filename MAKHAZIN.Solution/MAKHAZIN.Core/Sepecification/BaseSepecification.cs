using MAKHAZIN.Core.Entities;
using System.Linq.Expressions;

namespace MAKHAZIN.Core.Sepecification
{
    public class BaseSepecification<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; set; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSepecification()
        {
            // Criteria = null;
        }
        public BaseSepecification(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }
        public void AddOrderBy(Expression<Func<T,object>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void AddOrderByDesc(Expression<Func<T,object>> orderByDesc)
        {
            OrderByDescending = orderByDesc;
        }
        public void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginationEnabled = true;
        }
        
        /// <summary>
        /// Add a string-based include for nested navigation properties (e.g., "Bids.User")
        /// </summary>
        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
