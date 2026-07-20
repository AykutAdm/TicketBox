using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TicketBox.WebUI.DTOs.UserDtos;

namespace TicketBox.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    [Route("User/Profile")]
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = GetAuthenticatedClient();
            var responseMessage = await client.GetAsync("https://localhost:7171/api/Users/me");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UserProfileDto>(jsonData);
                return View(values);
            }
            return View();
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
