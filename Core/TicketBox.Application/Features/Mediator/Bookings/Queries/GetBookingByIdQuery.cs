using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Bookings.Results;

namespace TicketBox.Application.Features.Mediator.Bookings.Queries
{
    public class GetBookingByIdQuery : IRequest<GetBookingByIdQueryResult>
    {
        public int BookingId { get; set; }

        public GetBookingByIdQuery(int bookingId)
        {
            BookingId = bookingId;
        }
    }
}
