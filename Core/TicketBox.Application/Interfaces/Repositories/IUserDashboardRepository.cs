using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Interfaces.Repositories
{
    public interface IUserDashboardRepository
    {
        Task<int> CountTicketsAsync(string appUserId);
        Task<int> CountUpcomingTicketsAsync(string appUserId);
        Task<int> CountPastTicketsAsync(string appUserId);
        Task<int> CountBookingsAsync(string appUserId);
        Task<decimal> GetTotalSpentAsync(string appUserId);
    }
}
