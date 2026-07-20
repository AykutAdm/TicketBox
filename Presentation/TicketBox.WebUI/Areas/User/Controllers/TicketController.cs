using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TicketBox.WebUI.DTOs.TicketDtos;

namespace TicketBox.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    [Route("User/Ticket")]
    public class TicketController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TicketController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = GetAuthenticatedClient();
            var responseMessage = await client.GetAsync("https://localhost:7171/api/Tickets/my");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMyTicketDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("Download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var client = GetAuthenticatedClient();
            var responseMessage = await client.GetAsync($"https://localhost:7171/api/Tickets/{id}/download");
            var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
            var fileName = responseMessage.Content.Headers.ContentDisposition.FileName.Trim('"');
            return File(bytes, "image/png", fileName);
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
