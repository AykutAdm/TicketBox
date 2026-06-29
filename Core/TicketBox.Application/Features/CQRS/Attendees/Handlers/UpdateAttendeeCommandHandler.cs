using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Attendees.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class UpdateAttendeeCommandHandler
    {
        private readonly IAttendeeRepository _repository;
        private readonly IMapper _mapper;

        public UpdateAttendeeCommandHandler(IAttendeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAttendeeCommand command)
        {
            var values = await _repository.GetByIdAsync(command.AttendeeId);
            _mapper.Map(command, values);
            await _repository.UpdateAsync(values);
        }
    }
}
