using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Users.Queries;
using TicketBox.Application.Features.Mediator.Users.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Users.Handlers
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileQueryResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserProfileQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<GetUserProfileQueryResult> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            return _mapper.Map<GetUserProfileQueryResult>(user);

        }
    }
}

