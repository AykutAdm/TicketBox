using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Application.Features.Mediator.Bookings.Results;
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Application.Features.Mediator.Events.Results;
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
            CreateMap<Ticket, GetTicketQueryResult>()
                .ForMember(d => d.EventTitle, o => o.MapFrom(s => s.Booking.Event.Title))
                .ForMember(d => d.EventDate, o => o.MapFrom(s => s.Booking.Event.EventDate))
                .ForMember(d => d.Location, o => o.MapFrom(s => s.Booking.Event.Location))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Booking.AppUser.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Booking.AppUser.LastName));

            CreateMap<Ticket, GetTicketByIdQueryResult>()
                .ForMember(d => d.EventTitle, o => o.MapFrom(s => s.Booking.Event.Title))
                .ForMember(d => d.EventDate, o => o.MapFrom(s => s.Booking.Event.EventDate))
                .ForMember(d => d.Location, o => o.MapFrom(s => s.Booking.Event.Location))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Booking.AppUser.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Booking.AppUser.LastName));

            CreateMap<Ticket, GetMyTicketsQueryResult>()
                .ForMember(d => d.EventTitle, o => o.MapFrom(s => s.Booking.Event.Title))
                .ForMember(d => d.EventDate, o => o.MapFrom(s => s.Booking.Event.EventDate))
                .ForMember(d => d.Location, o => o.MapFrom(s => s.Booking.Event.Location));



            // Booking
            CreateMap<Booking, GetBookingQueryResult>()
                .ForMember(d => d.EventTitle, o => o.MapFrom(s => s.Event.Title))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.AppUser.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.AppUser.LastName));

            CreateMap<Booking, GetBookingByIdQueryResult>()
                .ForMember(d => d.EventTitle, o => o.MapFrom(s => s.Event.Title))
                .ForMember(d => d.EventDate, o => o.MapFrom(s => s.Event.EventDate))
                .ForMember(d => d.Location, o => o.MapFrom(s => s.Event.Location))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.AppUser.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.AppUser.LastName));

            CreateMap<Ticket, TicketSummary>();

        }
    }
}
