using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;


namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class CreateCategoryCommandHandler
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateCategoryCommand command)
        {
            var value = _mapper.Map<Category>(command);

            await _repository.CreateAsync(value);

        }
    }
}
