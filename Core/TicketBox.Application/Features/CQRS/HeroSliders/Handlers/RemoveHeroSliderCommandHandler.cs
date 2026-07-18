using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.HeroSliders.Commands;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.CQRS.HeroSliders.Handlers
{
    public class RemoveHeroSliderCommandHandler
    {
        private readonly IHeroSliderRepository _repository;

        public RemoveHeroSliderCommandHandler(IHeroSliderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveHeroSliderCommand command)
        {
            await _repository.DeleteAsync(command.HeroSliderId);
        }
    }
}
