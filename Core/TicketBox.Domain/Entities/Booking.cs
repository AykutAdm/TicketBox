using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
