using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Tickets.Commands
{
    public class RemoveTicketCommand : IRequest
    {
        public int TicketId { get; set; }

        public RemoveTicketCommand(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
