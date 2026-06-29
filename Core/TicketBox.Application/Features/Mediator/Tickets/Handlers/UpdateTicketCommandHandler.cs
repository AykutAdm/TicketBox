using AutoMapper;
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
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
    {
        private readonly ITicketRepository _repository;
        private readonly IMapper _mapper;

        public UpdateTicketCommandHandler(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.TicketId);
            _mapper.Map(request, value);
            await _repository.UpdateAsync(value);
        }
    }
}
