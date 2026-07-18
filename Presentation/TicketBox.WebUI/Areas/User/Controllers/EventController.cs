using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TicketBox.WebUI.DTOs.BookingDtos;
using TicketBox.WebUI.DTOs.EventDtos;

namespace TicketBox.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/Event")]
    public class EventController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EventController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Authorize]
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7171/api/Events");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEventDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        [Route("EventDetail/{id}")]
        public async Task<IActionResult> EventDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7171/api/Events/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetEventByIdDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("BuyTicket")]
        public async Task<IActionResult> BuyTicket(CreateBookingDto dto)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7171/api/Bookings", content);

            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index", "Ticket");

            TempData["Error"] = "Bilet alınamadı. Lütfen tekrar deneyin.";
            return RedirectToAction("EventDetail", new { id = dto.EventId });
        }
    }
}
