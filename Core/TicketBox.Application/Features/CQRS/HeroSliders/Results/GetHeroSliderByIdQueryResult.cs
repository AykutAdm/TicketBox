using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.HeroSliders.Results
{
    public class GetHeroSliderByIdQueryResult
    {
        public int HeroSliderId { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
