using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Tickets.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class RemoveTicketCommandHandler : IRequestHandler<RemoveTicketCommand>
    {
        private readonly ITicketRepository _repository;

        public RemoveTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveTicketCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.TicketId);
        }
    }
}
