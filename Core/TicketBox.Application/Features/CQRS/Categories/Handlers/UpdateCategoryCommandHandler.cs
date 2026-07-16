using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class UpdateCategoryCommandHandler
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateCategoryCommand> _validator;

        public UpdateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper, IValidator<UpdateCategoryCommand> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Handle(UpdateCategoryCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            var values = await _repository.GetByIdAsync(command.CategoryId);
            _mapper.Map(command, values);
            await _repository.UpdateAsync(values);
        }
    }
}
