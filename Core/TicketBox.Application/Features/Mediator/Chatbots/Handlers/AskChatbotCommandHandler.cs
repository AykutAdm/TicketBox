using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Chatbots.Commands;
using TicketBox.Application.Interfaces.Repositories;
using TicketBox.Application.Interfaces.Services;

namespace TicketBox.Application.Features.Mediator.Chatbots.Handlers
{
    public class AskChatbotCommandHandler : IRequestHandler<AskChatbotCommand, string>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IClaudeChatService _claudeChatService;
        private readonly ITavilySearchService _tavilySearchService;

        public AskChatbotCommandHandler(IEventRepository eventRepository, IClaudeChatService claudeChatService, ITavilySearchService tavilySearchService)
        {
            _eventRepository = eventRepository;
            _claudeChatService = claudeChatService;
            _tavilySearchService = tavilySearchService;
        }

        public async Task<string> Handle(AskChatbotCommand request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetAllAsync();

            var eventListText = new StringBuilder();
            foreach (var e in events)
            {
                eventListText.AppendLine($"- {e.Title} | Kategori: {e.Category.CategoryName} | Tarih: {e.EventDate:dd MMMM yyyy} | Lokasyon: {e.Location} | Fiyat: {e.Price}₺");
            }

            var liveInfo = string.Empty;
            if (request.EventId.HasValue)
            {
                var selectedEvent = events.FirstOrDefault(e => e.EventId == request.EventId.Value);
                if (selectedEvent != null)
                {
                    liveInfo = await _tavilySearchService.SearchAsync($"{selectedEvent.Title} haberleri ve detayları");
                }
            }

            var liveInfoBlock = string.IsNullOrEmpty(liveInfo)
                ? string.Empty
                : $"\nKullanıcının sorduğu etkinlik hakkında internetten bulduğun güncel bilgi:\n{liveInfo}\nBunu etkinlik öncesi tavsiyelerinde kullanabilirsin.\n";

            var systemPrompt = $@"
                Sen TicketBox adlı bilet satış platformunun sohbet asistanısın. Görevin, kullanıcının o anki ruh haline göre aşağıdaki GERÇEK etkinlik listesinden uygun olanları önermek ve etkinlik öncesi kısa tavsiyeler vermek.

                Kurallar:
                - Sadece aşağıdaki listede olan etkinlikleri öner, listede olmayan bir etkinlik uydurma.
                - Türkçe, samimi ve kısa cevaplar ver.
                - Kullanıcının ruh hali belirtilmişse ({request.Mood}), önerilerini ona göre şekillendir.

                Güncel etkinlikler:
                {eventListText}
                {liveInfoBlock}";

            return await _claudeChatService.AskAsync(systemPrompt, request.UserMessage);
        }
    }
}
