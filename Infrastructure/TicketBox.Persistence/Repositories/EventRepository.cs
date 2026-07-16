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
    public class EventRepository : IEventRepository
    {
        private readonly TicketContext _context;

        public EventRepository(TicketContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Event events)
        {
            await _context.Events.AddAsync(events);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var value = await _context.Events.FindAsync(id);

            if (value != null)
            {
                _context.Events.Remove(value);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events.Include(x => x.Category).ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.Include(x => x.Category).FirstOrDefaultAsync(y => y.EventId == id);
        }

        public async Task UpdateAsync(Event events)
        {
            _context.Events.Update(events);
            await _context.SaveChangesAsync();
        }
    }
}
