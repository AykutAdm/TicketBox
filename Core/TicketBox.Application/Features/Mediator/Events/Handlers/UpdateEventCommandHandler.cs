using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateEventCommand> _validator;

        public UpdateEventCommandHandler(IEventRepository repository, IMapper mapper, IValidator<UpdateEventCommand> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var value = await _repository.GetByIdAsync(request.EventId);
            _mapper.Map(request, value);
            await _repository.UpdateAsync(value);
        }
    }
}
