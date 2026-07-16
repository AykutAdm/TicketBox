using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Queries;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class GetCategoryByIdQueryHandler
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCategoryByIdQueryResult> Handle(GetCategoryByIdQuery query)
        {
            var value = await _repository.GetByIdAsync(query.CategoryId);

            return _mapper.Map<GetCategoryByIdQueryResult>(value);
        }

    }
}
