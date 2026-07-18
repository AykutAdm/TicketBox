using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Specifications;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
        Task CreateAsync(Event events);
        Task UpdateAsync(Event events);
        Task DeleteAsync(int id);
        Task<List<Event>> GetFeaturedEvents();
        Task<List<Event>> SearchAsync(ISpecification<Event> spec);
    }
}
