using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mappings
{
    public class GeneralMappingProfile : Profile
    {
        public GeneralMappingProfile()
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();

            CreateMap<GetCategoryQueryResult, Category>().ReverseMap();
            CreateMap<GetCategoryByIdQueryResult, Category>().ReverseMap();
        }
    }
}
