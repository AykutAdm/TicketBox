using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Bookings.Commands;
using TicketBox.Application.Interfaces.Repositories;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IValidator<CreateBookingCommand> _validator;

        public CreateBookingCommandHandler(IEventRepository eventRepository, IBookingRepository bookingRepository, ITicketRepository ticketRepository, IValidator<CreateBookingCommand> validator)
        {
            _eventRepository = eventRepository;
            _bookingRepository = bookingRepository;
            _ticketRepository = ticketRepository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            //Event var mý
            var eventEntity = await _eventRepository.GetByIdAsync(request.EventId);
            if (eventEntity == null)
            {
                throw new Exception("Etkinlik bulunamadý");
            }


            //Kontenjan yeterli mi
            if (eventEntity.Capacity < request.Quantity)
            {
                throw new Exception("Yetersiz kontenjan");
            }


            var booking = new Booking
            {
                AppUserId = request.AppUserId,
                EventId = request.EventId,
                Quantity = request.Quantity,
                TotalPrice = eventEntity.Price * request.Quantity,
                BookingDate = DateTime.Now
            };

            await _bookingRepository.CreateAsync(booking);


            //Event'in kalan kapasitesini düþür
            eventEntity.Capacity -= request.Quantity;
            await _eventRepository.UpdateAsync(eventEntity);

            //Quantity kadar Ticket üret
            for (int i = 0; i < request.Quantity; i++)
            {
                var ticket = new Ticket
                {
                    BookingId = booking.BookingId,
                    PNR = GeneratePnr(),
                    PurchaseDate = DateTime.UtcNow,
                    Price = eventEntity.Price
                };

                await _ticketRepository.CreateAsync(ticket);
            }
            return booking.BookingId;
        }

        private static string GeneratePnr()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
