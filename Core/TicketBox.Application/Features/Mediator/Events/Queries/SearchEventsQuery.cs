using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Events.Results;

namespace TicketBox.Application.Features.Mediator.Events.Queries
{
    public class SearchEventsQuery : IRequest<List<GetEventQueryResult>>
    {
        public int? CategoryId { get; set; }
        public string? Title { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
