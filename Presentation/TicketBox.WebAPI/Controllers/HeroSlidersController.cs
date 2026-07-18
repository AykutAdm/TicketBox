using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.CQRS.HeroSliders.Commands;
using TicketBox.Application.Features.CQRS.HeroSliders.Handlers;
using TicketBox.Application.Features.CQRS.HeroSliders.Queries;

namespace TicketBox.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroSlidersController : ControllerBase
    {
        private readonly CreateHeroSliderCommandHandler _createHeroSliderCommandHandler;
        private readonly UpdateHeroSliderCommandHandler _updateHeroSliderCommandHandler;
        private readonly RemoveHeroSliderCommandHandler _removeHeroSliderCommandHandler;
        private readonly GetHeroSliderQueryHandler _getHeroSliderCommandHandler;
        private readonly GetHeroSliderByIdQueryHandler _getHeroSliderByIdQueryHandler;

        public HeroSlidersController(CreateHeroSliderCommandHandler createHeroSliderCommandHandler, UpdateHeroSliderCommandHandler updateHeroSliderCommandHandler, RemoveHeroSliderCommandHandler removeHeroSliderCommandHandler, GetHeroSliderQueryHandler getHeroSliderCommandHandler, GetHeroSliderByIdQueryHandler getHeroSliderByIdQueryHandler)
        {
            _createHeroSliderCommandHandler = createHeroSliderCommandHandler;
            _updateHeroSliderCommandHandler = updateHeroSliderCommandHandler;
            _removeHeroSliderCommandHandler = removeHeroSliderCommandHandler;
            _getHeroSliderCommandHandler = getHeroSliderCommandHandler;
            _getHeroSliderByIdQueryHandler = getHeroSliderByIdQueryHandler;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllHeroSliders()
        {
            var values = await _getHeroSliderCommandHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHeroSlider(int id)
        {
            var value = await _getHeroSliderByIdQueryHandler.Handle(new GetHeroSliderByIdQuery(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHeroSlider(CreateHeroSliderCommand command)
        {
            await _createHeroSliderCommandHandler.Handle(command);
            return Ok("Slider Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveHeroSlider(int id)
        {
            await _removeHeroSliderCommandHandler.Handle(new RemoveHeroSliderCommand(id));
            return Ok("Slider Silindi");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateHeroSlider(UpdateHeroSliderCommand command)
        {
            await _updateHeroSliderCommandHandler.Handle(command);
            return Ok("Slider Güncellendi");
        }
    }
}
