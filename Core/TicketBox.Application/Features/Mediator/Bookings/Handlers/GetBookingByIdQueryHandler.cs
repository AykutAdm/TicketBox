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
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, GetBookingByIdQueryResult>
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public GetBookingByIdQueryHandler(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBookingByIdQueryResult> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.BookingId);
            return _mapper.Map<GetBookingByIdQueryResult>(value);
        }
    }
}
