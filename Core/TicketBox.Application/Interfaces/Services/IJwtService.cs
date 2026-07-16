using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Interfaces.Services
{
    public interface IJwtService
    {
        Task<string> GenerateToken(AppUser appUser);
    }
}
