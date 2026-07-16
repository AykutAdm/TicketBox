using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Auth.Commands;
using TicketBox.Application.Interfaces.Services;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IValidator<LoginCommand> _validator;

        public LoginCommandHandler(UserManager<AppUser> userManager, IJwtService jwtService, IValidator<LoginCommand> validator)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _validator = validator;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return null;

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid) return null;

            return await _jwtService.GenerateToken(user);
        }
    }
}
