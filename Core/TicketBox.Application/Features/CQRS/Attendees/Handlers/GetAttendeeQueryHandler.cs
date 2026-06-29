using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Attendees.Results;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class GetAttendeeQueryHandler
    {
        private readonly IAttendeeRepository _repository;
        private readonly IMapper _mapper;

        public GetAttendeeQueryHandler(IAttendeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAttendeeQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return _mapper.Map<List<GetAttendeeQueryResult>>(values);
        }
    }
}
