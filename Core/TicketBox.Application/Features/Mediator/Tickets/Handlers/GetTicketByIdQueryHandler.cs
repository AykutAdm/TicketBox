using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Tickets.Queries;
using TicketBox.Application.Features.Mediator.Tickets.Results;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, GetTicketByIdQueryResult>
    {
        private readonly ITicketRepository _repository;
        private readonly IMapper _mapper;

        public GetTicketByIdQueryHandler(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetTicketByIdQueryResult> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.TicketId);
            return _mapper.Map<GetTicketByIdQueryResult>(value);
        }
    }
}
