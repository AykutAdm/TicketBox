using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Interfaces.Repositories;
using TicketBox.Persistence.Context;

namespace TicketBox.Persistence.Repositories
{
    public class UserDashboardRepository : IUserDashboardRepository
    {
        private readonly TicketContext _context;

        public UserDashboardRepository(TicketContext context)
        {
            _context = context;
        }

        public async Task<int> CountTicketsAsync(string appUserId)
        {
            return await _context.Tickets.Where(x => x.Booking.AppUserId == appUserId).CountAsync();
        }

        public async Task<int> CountUpcomingTicketsAsync(string appUserId)
        {
            return await _context.Tickets.Where(x => x.Booking.AppUserId == appUserId && x.Booking.Event.EventDate >= DateTime.Now).CountAsync();
        }

        public async Task<int> CountPastTicketsAsync(string appUserId)
        {
            return await _context.Tickets.Where(x => x.Booking.AppUserId == appUserId && x.Booking.Event.EventDate < DateTime.Now).CountAsync();
        }

        public async Task<int> CountBookingsAsync(string appUserId)
        {
            return await _context.Bookings.Where(x => x.AppUserId == appUserId).CountAsync();
        }

        public async Task<decimal> GetTotalSpentAsync(string appUserId)
        {
            return await _context.Bookings.Where(x => x.AppUserId == appUserId).SumAsync(x => x.TotalPrice);
        }
    }
}
