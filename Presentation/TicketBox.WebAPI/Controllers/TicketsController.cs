using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetMyTickets([FromQuery] string appUserId)
        {
            var result = await _mediator.Send(new GetMyTicketsQuery { AppUserId = appUserId });
            return Ok(result);
        }
    }
}
