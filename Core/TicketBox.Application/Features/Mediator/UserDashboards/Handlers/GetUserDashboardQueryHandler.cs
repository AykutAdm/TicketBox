using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.UserDashboards.Queries;
using TicketBox.Application.Features.Mediator.UserDashboards.Results;
using TicketBox.Application.Interfaces.Repositories;

namespace TicketBox.Application.Features.Mediator.UserDashboards.Handlers
{
    public class GetUserDashboardQueryHandler : IRequestHandler<GetUserDashboardQuery, GetUserDashboardQueryResult>
    {
        private readonly IUserDashboardRepository _repository;

        public GetUserDashboardQueryHandler(IUserDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetUserDashboardQueryResult> Handle(GetUserDashboardQuery request, CancellationToken cancellationToken)
        {
            var result = new GetUserDashboardQueryResult
            {
                TicketCount = await _repository.CountTicketsAsync(request.AppUserId),
                UpcomingTicketCount = await _repository.CountUpcomingTicketsAsync(request.AppUserId),
                PastTicketCount = await _repository.CountPastTicketsAsync(request.AppUserId),
                BookingCount = await _repository.CountBookingsAsync(request.AppUserId),
                TotalSpent = await _repository.GetTotalSpentAsync(request.AppUserId)
            };

            return result;
        }
    }
}
