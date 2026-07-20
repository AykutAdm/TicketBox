using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Mediator.Chatbots.Commands
{
    public class AskChatbotCommand : IRequest<string>
    {
        public string UserMessage { get; set; }
        public string Mood { get; set; }
        public int? EventId { get; set; }
    }
}
