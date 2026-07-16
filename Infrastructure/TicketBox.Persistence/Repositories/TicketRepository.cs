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
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketContext _context;

        public TicketRepository(TicketContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var value = await _context.Tickets.FindAsync(id);

            if (value != null)
            {
                _context.Tickets.Remove(value);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Tickets
               .Include(x => x.Booking)
                   .ThenInclude(b => b.AppUser)
               .Include(x => x.Booking)
                   .ThenInclude(b => b.Event)
               .ToListAsync();
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _context.Tickets
                .Include(x => x.Booking)
                    .ThenInclude(b => b.AppUser)
                .Include(x => x.Booking)
                    .ThenInclude(b => b.Event)
                .FirstOrDefaultAsync(z => z.TicketId == id);
        }

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(string appUserId)
        {
            return await _context.Tickets
                .Include(x => x.Booking)
                    .ThenInclude(b => b.AppUser)
                .Include(x => x.Booking)
                    .ThenInclude(b => b.Event)
                .Where(t => t.Booking.AppUserId == appUserId).ToListAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
