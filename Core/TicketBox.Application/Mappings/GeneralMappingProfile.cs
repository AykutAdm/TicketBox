using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Attendees.Commands;
using TicketBox.Application.Features.CQRS.Attendees.Results;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mappings
{
    public class GeneralMappingProfile : Profile
    {
        public GeneralMappingProfile()
        {
            //Category
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();

            CreateMap<GetCategoryQueryResult, Category>().ReverseMap();
            CreateMap<GetCategoryByIdQueryResult, Category>().ReverseMap();



            //Attendee
            CreateMap<CreateAttendeeCommand, Attendee>();
            CreateMap<UpdateAttendeeCommand, Attendee>();

            CreateMap<GetAttendeeQueryResult, Attendee>().ReverseMap();
            CreateMap<GetAttendeeByIdQueryResult, Attendee>().ReverseMap();
        }
    }
}
