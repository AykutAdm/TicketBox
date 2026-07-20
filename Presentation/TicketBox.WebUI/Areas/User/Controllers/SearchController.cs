using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TicketBox.WebUI.DTOs.CategoryDtos;
using TicketBox.WebUI.DTOs.EventDtos;

namespace TicketBox.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    [Route("User/Search")]
    public class SearchController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SearchController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index(string title, int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var client = GetAuthenticatedClient();

            string url = "https://localhost:7171/api/Events/search?";

            if (!string.IsNullOrEmpty(title))
                url += $"title={title}&";

            if (categoryId.HasValue)
                url += $"categoryId={categoryId}&";

            if (minPrice.HasValue)
                url += $"minPrice={minPrice}&";

            if (maxPrice.HasValue)
                url += $"maxPrice={maxPrice}&";

            url = url.TrimEnd('&', '?');

            List<ResultEventDto> values = new();

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultEventDto>>(json) ?? new();
            }

            var categoryResponse = await client.GetAsync("https://localhost:7171/api/Categories");

            if (categoryResponse.IsSuccessStatusCode)
            {
                var json = await categoryResponse.Content.ReadAsStringAsync();
                ViewBag.Categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(json);
            }

            ViewBag.TitleFilter = title;
            ViewBag.CategoryId = categoryId;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            return View(values);
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
