using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Interfaces
{
    public interface IAttendeeRepository
    {
        Task<List<Attendee>> GetAllAsync();
        Task<Attendee> GetByIdAsync(int id);
        Task CreateAsync(Attendee attendee);
        Task UpdateAsync(Attendee attendee);
        Task DeleteAsync(int id);
    }
}
