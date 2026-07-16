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
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateEventCommand> _validator;

        public CreateEventCommandHandler(IEventRepository repository, IMapper mapper, IValidator<CreateEventCommand> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var value = _mapper.Map<Event>(request);
            await _repository.CreateAsync(value);
        }
    }
}
