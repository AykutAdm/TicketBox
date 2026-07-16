using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Interfaces.Repositories;
using TicketBox.Domain.Entities;
using TicketBox.Persistence.Context;

namespace TicketBox.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TicketContext _context;

        public BookingRepository(TicketContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await GetByIdAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings
               .Include(x => x.AppUser)
               .Include(x => x.Event)
               .Include(x => x.Tickets)
               .ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(x => x.AppUser)
                .Include(x => x.Event)
                .Include(x => x.Tickets)
                .FirstOrDefaultAsync(y => y.BookingId == id);
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}
