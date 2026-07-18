using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.HeroSliders.Commands
{
    public class UpdateHeroSliderCommand
    {
        public int HeroSliderId { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
