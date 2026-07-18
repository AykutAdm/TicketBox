using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.HeroSliders.Commands
{
    public class RemoveHeroSliderCommand
    {
        public int HeroSliderId { get; set; }

        public RemoveHeroSliderCommand(int heroSliderId)
        {
            HeroSliderId = heroSliderId;
        }
    }
}
