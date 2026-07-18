using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Events.Queries;
using TicketBox.Application.Features.Mediator.Events.Results;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class GetFeaturedEventsQueryHandler : IRequestHandler<GetFeaturedEventsQuery, List<GetFeaturedEventsQueryResult>>
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;

        public GetFeaturedEventsQueryHandler(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetFeaturedEventsQueryResult>> Handle(GetFeaturedEventsQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetFeaturedEvents();
            return _mapper.Map<List<GetFeaturedEventsQueryResult>>(values);
        }
    }
}
