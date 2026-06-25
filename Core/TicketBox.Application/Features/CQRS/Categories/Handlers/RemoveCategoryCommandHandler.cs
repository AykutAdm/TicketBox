using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class RemoveCategoryCommandHandler
    {
        private readonly ICategoryRepository _repository;

        public RemoveCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveCategoryCommand command)
        {
            await _repository.DeleteAsync(command.CategoryId);
        }
    }
}
