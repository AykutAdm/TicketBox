using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class RemoveEventCommandHandler : IRequestHandler<RemoveEventCommand>
    {
        private readonly IEventRepository _repository;

        public RemoveEventCommandHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveEventCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.EventId);
        }
    }
}
