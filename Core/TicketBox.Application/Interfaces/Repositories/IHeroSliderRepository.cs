using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Interfaces.Repositories
{
    public interface IHeroSliderRepository
    {
        Task<List<HeroSlider>> GetAllAsync();
        Task<HeroSlider> GetByIdAsync(int id);
        Task CreateAsync(HeroSlider heroSlider);
        Task UpdateAsync(HeroSlider heroSlider);
        Task DeleteAsync(int id);
    }
}
