using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Mediator.Tickets.Commands
{
    public class UpdateTicketCommand : IRequest
    {
        public int TicketId { get; set; }

        public int EventId { get; set; }

        public int AttendeeId { get; set; }

        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}
