using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBox.Application.Features.Mediator.Bookings.Commands;
using TicketBox.Application.Features.Mediator.Bookings.Queries;

namespace TicketBox.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _mediator.Send(new GetBookingQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var result = await _mediator.Send(new GetBookingByIdQuery(id));

            return Ok(result);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingCommand command)
        {
            command.AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var firstName = User.FindFirstValue(ClaimTypes.Name);
            var lastName = User.FindFirstValue(ClaimTypes.Surname);
            command.UserName = $"{firstName} {lastName}".Trim();

            var bookingId = await _mediator.Send(command);
            return Ok(new { BookingId = bookingId });
        }
    }
}
