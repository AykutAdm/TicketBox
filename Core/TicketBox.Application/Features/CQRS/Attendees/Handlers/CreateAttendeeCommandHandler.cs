using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Attendees.Commands;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class CreateAttendeeCommandHandler
    {
        private readonly IAttendeeRepository _repository;
        private readonly IMapper _mapper;

        public CreateAttendeeCommandHandler(IAttendeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateAttendeeCommand command)
        {
            var value = _mapper.Map<Attendee>(command);

            await _repository.CreateAsync(value);

        }
    }
}
