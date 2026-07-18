using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.HeroSliders.Commands;
using TicketBox.Application.Interfaces.Repositories;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.HeroSliders.Handlers
{
    public class CreateHeroSliderCommandHandler
    {
        private readonly IHeroSliderRepository _repository;
        private readonly IMapper _mapper;

        public CreateHeroSliderCommandHandler(IHeroSliderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateHeroSliderCommand command)
        {
            var value = _mapper.Map<HeroSlider>(command);
            await _repository.CreateAsync(value);
        }
    }
}
