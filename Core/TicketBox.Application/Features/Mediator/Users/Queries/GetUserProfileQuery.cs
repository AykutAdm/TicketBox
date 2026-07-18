using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Users.Results;

namespace TicketBox.Application.Features.Mediator.Users.Queries
{
    public class GetUserProfileQuery : IRequest<GetUserProfileQueryResult>
    {
        public string UserId { get; set; }

        public GetUserProfileQuery(string userId)
        {
            UserId = userId;
        }
    }
}
