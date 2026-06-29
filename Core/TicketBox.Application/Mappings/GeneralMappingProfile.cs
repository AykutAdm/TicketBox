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
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Application.Features.Mediator.Events.Results;
using TicketBox.Application.Features.Mediator.Tickets.Commands;
using TicketBox.Application.Features.Mediator.Tickets.Results;
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



            //Event
            CreateMap<CreateEventCommand, Event>();
            CreateMap<UpdateEventCommand, Event>();

            CreateMap<Event, GetEventQueryResult>()
                .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.CategoryName));
            CreateMap<Event, GetEventByIdQueryResult>()
                   .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.CategoryName));

            //Ticket
            CreateMap<CreateTicketCommand, Ticket>();
            CreateMap<UpdateTicketCommand, Ticket>();

            CreateMap<Ticket, GetTicketQueryResult>()
                .ForMember(dest => dest.EventName,
                opt => opt.MapFrom(src => src.Event.Title))
                .ForMember(dest => dest.AttendeeName,
                opt => opt.MapFrom(src => src.Attendee.Name));

            CreateMap<Ticket, GetTicketByIdQueryResult>()
                 .ForMember(dest => dest.EventName,
                opt => opt.MapFrom(src => src.Event.Title))
                 .ForMember(dest => dest.AttendeeName,
                opt => opt.MapFrom(src => src.Attendee.Name));



        }
    }
}
