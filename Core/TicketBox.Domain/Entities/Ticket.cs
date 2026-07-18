using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public string PNR { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
