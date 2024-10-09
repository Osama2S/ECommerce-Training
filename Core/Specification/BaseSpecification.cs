using System.Linq.Expressions;

namespace Core.Specification
{
#nullable disable
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification() { }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria=criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool isPagingEnable { get; private set; }

        protected void AddIncludes(Expression<Func<T, object>> Include)
        {
            Includes.Add(Include);
        }
        protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy=OrderByExpression;
        }
        protected void AddOrderByDecending(Expression<Func<T, object>> OrderByDecendingExpression)
        {
            OrderByDescending=OrderByDecendingExpression;
        }
        protected void ApplyPaging(int take,int skip)
        {
            Take=take;
            Skip=skip;
            isPagingEnable=true;
        }
    }
}
