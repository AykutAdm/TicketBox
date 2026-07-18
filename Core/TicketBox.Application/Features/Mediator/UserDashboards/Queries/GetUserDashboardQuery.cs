using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.UserDashboards.Results;

namespace TicketBox.Application.Features.Mediator.UserDashboards.Queries
{
    public class GetUserDashboardQuery : IRequest<GetUserDashboardQueryResult>
    {
        public string AppUserId { get; set; }

        public GetUserDashboardQuery(string appUserId)
        {
            AppUserId = appUserId;
        }
    }
}
