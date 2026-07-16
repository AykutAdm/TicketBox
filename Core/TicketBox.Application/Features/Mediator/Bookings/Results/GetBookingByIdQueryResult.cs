using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Mediator.Bookings.Results
{
    public class GetBookingByIdQueryResult
    {
        public int BookingId { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }

        public List<TicketSummary> Tickets { get; set; }
    }

    public class TicketSummary
    {
        public int TicketId { get; set; }
        public string PNR { get; set; }
        public decimal Price { get; set; }
    }
}
