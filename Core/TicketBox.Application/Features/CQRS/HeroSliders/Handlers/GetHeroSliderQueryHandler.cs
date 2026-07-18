using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.HeroSliders.Results;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.CQRS.HeroSliders.Handlers
{
    public class GetHeroSliderQueryHandler
    {
        private readonly IHeroSliderRepository _repository;
        private readonly IMapper _mapper;

        public GetHeroSliderQueryHandler(IHeroSliderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetHeroSliderQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return _mapper.Map<List<GetHeroSliderQueryResult>>(values);
        }
    }
}
