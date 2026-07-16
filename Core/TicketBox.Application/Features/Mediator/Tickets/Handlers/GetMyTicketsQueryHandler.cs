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
    public class GetMyTicketsQueryHandler : IRequestHandler<GetMyTicketsQuery, List<GetMyTicketsQueryResult>>
    {
        private readonly ITicketRepository _repository;
        private readonly IMapper _mapper;

        public GetMyTicketsQueryHandler(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetMyTicketsQueryResult>> Handle(GetMyTicketsQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetTicketsByUserIdAsync(request.AppUserId);
            return _mapper.Map<List<GetMyTicketsQueryResult>>(values);
        }
    }
}
