using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;
using TicketBox.Persistence.Context;

namespace TicketBox.Persistence.Repositories
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly TicketContext _context;

        public AttendeeRepository(TicketContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Attendee attendee)
        {
            await _context.Attendees.AddAsync(attendee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var value = await _context.Attendees.FindAsync(id);

            if (value != null)
            {
                _context.Attendees.Remove(value);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Attendee>> GetAllAsync()
        {
            return await _context.Attendees.ToListAsync();
        }

        public async Task<Attendee> GetByIdAsync(int id)
        {
            return await _context.Attendees.FindAsync(id);
        }

        public async Task UpdateAsync(Attendee attendee)
        {
            _context.Attendees.Update(attendee);
            await _context.SaveChangesAsync();
        }
    }
}
