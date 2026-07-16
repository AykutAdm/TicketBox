using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Tickets.Results;

namespace TicketBox.Application.Features.Mediator.Tickets.Queries
{
    public class GetMyTicketsQuery : IRequest<List<GetMyTicketsQueryResult>>
    {
        public string AppUserId { get; set; }
    }
}
