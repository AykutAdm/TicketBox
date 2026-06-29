using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Tickets.Results
{
    public class GetTicketByIdQueryResult
    {
        public int TicketId { get; set; }

        public int EventId { get; set; }
        public string EventName { get; set; }

        public int AttendeeId { get; set; }
        public string AttendeeName { get; set; }

        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}
