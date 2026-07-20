using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TicketBox.WebUI.DTOs.TicketDtos;
using TicketBox.WebUI.DTOs.UserDtos;

namespace TicketBox.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    [Route("User/Dashboard")]
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = GetAuthenticatedClient();

            var page = new UserDashboardPageDto
            {
                Stats = new ResultUserDashboardDto(),
                UpcomingTickets = new List<ResultMyTicketDto>()
            };

            var dashboardResponse = await client.GetAsync("https://localhost:7171/api/Users/me/dashboard");
            if (dashboardResponse.IsSuccessStatusCode)
            {
                var jsonData = await dashboardResponse.Content.ReadAsStringAsync();
                page.Stats = JsonConvert.DeserializeObject<ResultUserDashboardDto>(jsonData);
            }

            var ticketResponse = await client.GetAsync("https://localhost:7171/api/Tickets/my");
            if (ticketResponse.IsSuccessStatusCode)
            {
                var jsonData = await ticketResponse.Content.ReadAsStringAsync();
                var tickets = JsonConvert.DeserializeObject<List<ResultMyTicketDto>>(jsonData);
                page.UpcomingTickets = tickets
                    .Where(t => t.EventDate >= DateTime.Now)
                    .OrderBy(t => t.EventDate)
                    .Take(4)
                    .ToList();
            }

            return View(page);
        }

        private HttpClient GetAuthenticatedClient()
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }
    }
}
