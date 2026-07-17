using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Specifications;

namespace TicketBox.Persistence.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
        {
            foreach (var criteria in specification.Criteria)
            {
                query = query.Where(criteria);
            }

            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
