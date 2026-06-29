using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Mediator.Events.Commands
{
    public class RemoveEventCommand : IRequest
    {
        public int EventId { get; set; }

        public RemoveEventCommand(int eventId)
        {
            EventId = eventId;
        }
    }
}
