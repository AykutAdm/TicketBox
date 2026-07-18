using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Mediator.UserDashboards.Results
{
    public class GetUserDashboardQueryResult
    {
        public int TicketCount { get; set; }
        public int UpcomingTicketCount { get; set; }
        public int PastTicketCount { get; set; }
        public int BookingCount { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
