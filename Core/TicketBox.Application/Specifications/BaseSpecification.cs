using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criteria { get; } = new();
        public List<Expression<Func<T, object>>> Includes { get; } = new();

        protected BaseSpecification()
        {
        }

        protected void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria.Add(criteria);
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

    }
}
