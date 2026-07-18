using FluentValidation;
using MediatR;
using System.Globalization;
using TicketBox.Application.Features.Mediator.Bookings.Commands;
using TicketBox.Application.Interfaces.Repositories;
using TicketBox.Application.Interfaces.Services;
using TicketBox.Application.Models;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketImageService _ticketImageService;
        private readonly IEmailService _emailService;
        private readonly IValidator<CreateBookingCommand> _validator;

        public CreateBookingCommandHandler(
            IEventRepository eventRepository,
            IBookingRepository bookingRepository,
            ITicketRepository ticketRepository,
            ITicketImageService ticketImageService,
            IValidator<CreateBookingCommand> validator,
            IEmailService emailService)
        {
            _eventRepository = eventRepository;
            _bookingRepository = bookingRepository;
            _ticketRepository = ticketRepository;
            _ticketImageService = ticketImageService;
            _validator = validator;
            _emailService = emailService;
        }

        public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var eventEntity = await _eventRepository.GetByIdAsync(request.EventId);
            if (eventEntity == null)
            {
                throw new Exception("Etkinlik bulunamadý");
            }

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

            eventEntity.Capacity -= request.Quantity;
            await _eventRepository.UpdateAsync(eventEntity);

            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var culture = new CultureInfo("tr-TR");
            var userName = string.IsNullOrWhiteSpace(request.UserName) ? "Misafir" : request.UserName;

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

                var imageData = new TicketImageData
                {
                    Pnr = ticket.PNR,
                    EventTitle = eventEntity.Title,
                    EventDate = eventEntity.EventDate.ToString("dd MMMM yyyy, HH:mm", culture),
                    Location = eventEntity.Location,
                    UserName = userName
                };

                ticket.ImageUrl = _ticketImageService.SaveTicketImage(imageData, webRootPath);

                await _ticketRepository.UpdateAsync(ticket);

                await _emailService.SendTicketEmailAsync(request.Email, "Biletiniz Hazýr", "Merhaba, biletiniz ektedir.", Path.Combine(webRootPath, "tickets", ticket.PNR + ".png"));
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
