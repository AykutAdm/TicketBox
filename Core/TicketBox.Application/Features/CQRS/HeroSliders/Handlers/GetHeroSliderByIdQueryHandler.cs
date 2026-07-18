using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.HeroSliders.Queries;
using TicketBox.Application.Features.CQRS.HeroSliders.Results;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.CQRS.HeroSliders.Handlers
{
    public class GetHeroSliderByIdQueryHandler
    {
        private readonly IHeroSliderRepository _repository;
        private readonly IMapper _mapper;

        public GetHeroSliderByIdQueryHandler(IHeroSliderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetHeroSliderByIdQueryResult> Handle(GetHeroSliderByIdQuery query)
        {
            var value = await _repository.GetByIdAsync(query.HeroSliderId);
            return _mapper.Map<GetHeroSliderByIdQueryResult>(value);
        }
    }
}
