using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Interfaces.Repositories;
using TicketBox.Domain.Entities;


namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class CreateCategoryCommandHandler
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryCommand> _validator;

        public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper, IValidator<CreateCategoryCommand> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Handle(CreateCategoryCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var value = _mapper.Map<Category>(command);
            await _repository.CreateAsync(value);

        }
    }
}
