using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Mediator.Auth.Commands;

namespace TicketBox.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest("Registration failed.");
            }
            return Ok("Registration successful.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var token = await _mediator.Send(command);
            if (token == null)
            {
                return Unauthorized("Email or password is incorrect.");
            }

            return Ok(new { token });
        }
    }
}
