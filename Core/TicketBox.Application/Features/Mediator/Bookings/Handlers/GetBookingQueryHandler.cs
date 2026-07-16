using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Bookings.Queries;
using TicketBox.Application.Features.Mediator.Bookings.Results;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, List<GetBookingQueryResult>>
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public GetBookingQueryHandler(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBookingQueryResult>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return _mapper.Map<List<GetBookingQueryResult>>(values);
        }
    }
}
