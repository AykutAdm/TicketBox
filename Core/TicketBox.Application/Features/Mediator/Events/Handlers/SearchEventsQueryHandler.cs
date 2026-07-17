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
using TicketBox.Application.Specifications;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class SearchEventsQueryHandler : IRequestHandler<SearchEventsQuery, List<GetEventQueryResult>>
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;

        public SearchEventsQueryHandler(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetEventQueryResult>> Handle(SearchEventsQuery request, CancellationToken cancellationToken)
        {
            var specification = new EventSearchSpecification(request.CategoryId, request.Title, request.MinPrice, request.MaxPrice);
            var values = await _repository.SearchAsync(specification);
            return _mapper.Map<List<GetEventQueryResult>>(values);
        }
    }
}
