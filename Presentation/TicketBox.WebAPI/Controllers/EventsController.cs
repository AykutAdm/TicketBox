using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Application.Features.Mediator.Events.Queries;

namespace TicketBox.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _mediator.Send(new GetEventQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var result = await _mediator.Send(new GetEventByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventCommand command)
        {
            await _mediator.Send(command);
            return Ok("Etkinlik eklendi.");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateEvent(UpdateEventCommand command)
        {
            await _mediator.Send(command);
            return Ok("Etkinlik güncellendi.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEvent(int id)
        {
            await _mediator.Send(new RemoveEventCommand(id));
            return Ok("Etkinlik silindi.");
        }
    }
}
