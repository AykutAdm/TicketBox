using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Bookings.Results;

namespace TicketBox.Application.Features.Mediator.Bookings.Queries
{
    public class GetBookingQuery : IRequest<List<GetBookingQueryResult>>
    {
    }
}
