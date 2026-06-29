using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Attendees.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class RemoveAttendeeCommandHandler
    {
        private readonly IAttendeeRepository _repository;

        public RemoveAttendeeCommandHandler(IAttendeeRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveAttendeeCommand command)
        {
            await _repository.DeleteAsync(command.AttendeeId);
        }
    }
}
