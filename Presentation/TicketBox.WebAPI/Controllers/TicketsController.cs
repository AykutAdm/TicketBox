using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBox.Application.Features.Mediator.Tickets.Queries;

namespace TicketBox.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var result = await _mediator.Send(new GetTicketQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var result = await _mediator.Send(new GetTicketByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyTickets()
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new GetMyTicketsQuery { AppUserId = appUserId });
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadTicket(int id)
        {
            var ticket = await _mediator.Send(new GetTicketByIdQuery(id));
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "tickets", ticket.PNR + ".png");
            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "image/png", ticket.PNR + ".png");
        }
    }
}
