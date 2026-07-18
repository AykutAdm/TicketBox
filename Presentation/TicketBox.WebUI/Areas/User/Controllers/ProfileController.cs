using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TicketBox.WebUI.DTOs.UserDtos;

namespace TicketBox.WebUI.Areas.User.Controllers
{
    [Area("User")]
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
            var token = HttpContext.Session.GetString("JwtToken");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.GetAsync("https://localhost:7171/api/Users/me");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UserProfileDto>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
