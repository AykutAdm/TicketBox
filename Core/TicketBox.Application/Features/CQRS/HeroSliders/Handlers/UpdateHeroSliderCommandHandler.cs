using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.HeroSliders.Commands;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.CQRS.HeroSliders.Handlers
{
    public class UpdateHeroSliderCommandHandler
    {
        private readonly IHeroSliderRepository _repository;
        private readonly IMapper _mapper;

        public UpdateHeroSliderCommandHandler(IHeroSliderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateHeroSliderCommand command)
        {
            var value = await _repository.GetByIdAsync(command.HeroSliderId);
            _mapper.Map(command, value);
            await _repository.UpdateAsync(value);
        }
    }
}
