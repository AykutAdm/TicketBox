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
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, List<GetTicketQueryResult>>
    {
        private readonly ITicketRepository _repository;
        private readonly IMapper _mapper;

        public GetTicketQueryHandler(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return _mapper.Map<List<GetTicketQueryResult>>(values);
        }
    }
}
