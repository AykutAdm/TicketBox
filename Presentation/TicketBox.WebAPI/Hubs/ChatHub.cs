using MediatR;
using Microsoft.AspNetCore.SignalR;
using TicketBox.Application.Features.Mediator.Chatbots.Commands;

namespace TicketBox.WebAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessage(string userMessage, string mood, int? eventId)
        {
            try
            {
                var reply = await _mediator.Send(new AskChatbotCommand { UserMessage = userMessage, Mood = mood, EventId = eventId });
                await Clients.Caller.SendAsync("ReceiveMessage", reply);
            }
            catch (Exception)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "Şu an yanıt veremiyorum, biraz sonra tekrar dener misin?");
            }
        }
    }
}
