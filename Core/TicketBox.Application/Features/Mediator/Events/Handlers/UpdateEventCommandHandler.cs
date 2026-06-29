using AutoMapper;
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
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.EventId);
            _mapper.Map(request, value);
            await _repository.UpdateAsync(value);
        }
    }
}
