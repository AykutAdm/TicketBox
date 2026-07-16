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
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, GetEventByIdQueryResult>
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;

        public GetEventByIdQueryHandler(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetEventByIdQueryResult> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.EventId);
            return _mapper.Map<GetEventByIdQueryResult>(value);
        }
    }
}
