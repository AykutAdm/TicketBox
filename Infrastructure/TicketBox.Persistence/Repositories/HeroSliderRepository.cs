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
    public class HeroSliderRepository : IHeroSliderRepository
    {
        private readonly TicketContext _context;

        public HeroSliderRepository(TicketContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(HeroSlider heroSlider)
        {
            await _context.HeroSliders.AddAsync(heroSlider);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var value = await _context.HeroSliders.FindAsync(id);
            _context.HeroSliders.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HeroSlider>> GetAllAsync()
        {
            return await _context.HeroSliders.AsNoTracking().ToListAsync();
        }

        public async Task<HeroSlider> GetByIdAsync(int id)
        {
            return await _context.HeroSliders.AsNoTracking().FirstOrDefaultAsync(x => x.HeroSliderId == id);
        }

        public async Task UpdateAsync(HeroSlider heroSlider)
        {
            _context.HeroSliders.Update(heroSlider);
            await _context.SaveChangesAsync();
        }
    }
}
