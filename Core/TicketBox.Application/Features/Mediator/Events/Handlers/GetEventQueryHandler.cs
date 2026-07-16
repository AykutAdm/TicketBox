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
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, List<GetEventQueryResult>>
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;

        public GetEventQueryHandler(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetEventQueryResult>> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return _mapper.Map<List<GetEventQueryResult>>(values);
        }
    }
}
