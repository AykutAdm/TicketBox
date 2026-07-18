using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.HeroSliders.Queries
{
    public class GetHeroSliderByIdQuery
    {
        public int HeroSliderId { get; set; }

        public GetHeroSliderByIdQuery(int heroSliderId)
        {
            HeroSliderId = heroSliderId;
        }
    }
}
