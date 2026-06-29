using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.CQRS.Attendees.Commands;
using TicketBox.Application.Features.CQRS.Attendees.Handlers;
using TicketBox.Application.Features.CQRS.Attendees.Queries;
namespace TicketBox.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        private readonly CreateAttendeeCommandHandler _createAttendeeCommandHandler;
        private readonly UpdateAttendeeCommandHandler _updateAttendeeCommandHandler;
        private readonly RemoveAttendeeCommandHandler _removeAttendeeCommandHandler;
        private readonly GetAttendeeQueryHandler _getAttendeeQueryHandler;
        private readonly GetAttendeeByIdQueryHandler _getAttendeeByIdQueryHandler;

        public AttendeesController(CreateAttendeeCommandHandler createAttendeeCommandHandler, UpdateAttendeeCommandHandler updateAttendeeCommandHandler, RemoveAttendeeCommandHandler removeAttendeeCommandHandler, GetAttendeeQueryHandler getAttendeeQueryHandler, GetAttendeeByIdQueryHandler getAttendeeByIdQueryHandler)
        {
            _createAttendeeCommandHandler = createAttendeeCommandHandler;
            _updateAttendeeCommandHandler = updateAttendeeCommandHandler;
            _removeAttendeeCommandHandler = removeAttendeeCommandHandler;
            _getAttendeeQueryHandler = getAttendeeQueryHandler;
            _getAttendeeByIdQueryHandler = getAttendeeByIdQueryHandler;
        }


        [HttpGet]
        public async Task<IActionResult> AttendeeList()
        {
            var values = await _getAttendeeQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendee(int id)
        {
            var value = await _getAttendeeByIdQueryHandler.Handle(new GetAttendeeByIdQuery(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendee(CreateAttendeeCommand command)
        {
            await _createAttendeeCommandHandler.Handle(command);
            return Ok("Katılımcı Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAttendee(int id)
        {
            await _removeAttendeeCommandHandler.Handle(new RemoveAttendeeCommand(id));
            return Ok("Katılımcı Silindi");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAttendee(UpdateAttendeeCommand command)
        {
            await _updateAttendeeCommandHandler.Handle(command);
            return Ok("Katılımcı Güncellendi");
        }


    }
}
