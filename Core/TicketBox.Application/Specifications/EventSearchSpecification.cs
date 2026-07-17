using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Specifications
{
    public class EventSearchSpecification : BaseSpecification<Event>
    {
        public EventSearchSpecification(int? categoryId, string? title, decimal? minPrice, decimal? maxPrice)
        {
            if (categoryId.HasValue)
            {
                AddCriteria(e => e.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                AddCriteria(e => e.Title.Contains(title));
            }

            if (minPrice.HasValue)
            {
                AddCriteria(e => e.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                AddCriteria(e => e.Price <= maxPrice.Value);
            }

            AddInclude(e => e.Category);
        }
    }
}
