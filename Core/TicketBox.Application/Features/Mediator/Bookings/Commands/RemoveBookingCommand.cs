using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Mediator.Bookings.Commands
{
    public class RemoveBookingCommand : IRequest
    {
        public int BookingId { get; set; }

        public RemoveBookingCommand(int bookingId)
        {
            BookingId = bookingId;
        }
    }
}
