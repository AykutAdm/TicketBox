using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketBox.WebUI.DTOs.HeroSliderDtos;

namespace TicketBox.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultHeroComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7171/api/HeroSliders");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultHeroSliderDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
