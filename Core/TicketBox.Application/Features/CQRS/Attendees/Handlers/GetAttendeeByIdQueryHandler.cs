using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Attendees.Queries;
using TicketBox.Application.Features.CQRS.Attendees.Results;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class GetAttendeeByIdQueryHandler
    {
        private readonly IAttendeeRepository _repository;
        private readonly IMapper _mapper;

        public GetAttendeeByIdQueryHandler(IAttendeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetAttendeeByIdQueryResult> Handle(GetAttendeeByIdQuery query)
        {
            var value = await _repository.GetByIdAsync(query.AttendeeId);

            return _mapper.Map<GetAttendeeByIdQueryResult>(value);
        }
    }
}
