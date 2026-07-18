using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendTicketEmailAsync(string toEmail, string subject, string body, string attachmentPath);
    }
}
