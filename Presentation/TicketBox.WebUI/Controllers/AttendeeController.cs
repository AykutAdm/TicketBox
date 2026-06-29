using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TicketBox.WebUI.DTOs.AttendeeDtos;

namespace TicketBox.WebUI.Controllers
{
    public class AttendeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AttendeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> AttendeeList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7171/api/Attendees");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAttendeeDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
