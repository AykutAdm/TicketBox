using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Mediator.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<int>
    {
        public string AppUserId { get; set; }
        public int EventId { get; set; }
        public int Quantity { get; set; }
    }
}
