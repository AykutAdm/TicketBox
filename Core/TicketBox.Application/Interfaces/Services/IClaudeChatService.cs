using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Interfaces.Services
{
    public interface IClaudeChatService
    {
        Task<string> AskAsync(string systemPrompt, string userMessage);
    }
}
