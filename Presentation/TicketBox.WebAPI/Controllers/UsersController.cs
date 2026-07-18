using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBox.Application.Features.Mediator.Users.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TicketBox.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new GetUserProfileQuery(userId));
            return Ok(result);
        }
    }
}
