using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.Attendees.Queries
{
    public class GetAttendeeByIdQuery
    {
        public int AttendeeId { get; set; }

        public GetAttendeeByIdQuery(int attendeeId)
        {
            AttendeeId = attendeeId;
        }
    }
}
