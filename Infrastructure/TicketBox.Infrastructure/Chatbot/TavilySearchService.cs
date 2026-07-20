using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Interfaces.Services;

namespace TicketBox.Infrastructure.Chatbot
{
    public class TavilySearchService : ITavilySearchService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TavilySearchService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> SearchAsync(string query)
        {
            var apiKey = _configuration["TavilySettings:ApiKey"];

            var requestBody = new
            {
                query = query,
                max_results = 3,
                include_answer = true
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await client.PostAsync("https://api.tavily.com/search", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Tavily API hatası: {responseJson}");

            var result = JsonConvert.DeserializeObject<TavilySearchResponse>(responseJson);

            var summary = new StringBuilder();
            if (!string.IsNullOrEmpty(result.Answer))
                summary.AppendLine(result.Answer);

            foreach (var r in result.Results)
                summary.AppendLine($"- {r.Title}: {r.Content}");

            return summary.ToString();
        }


        public class TavilySearchResponse
        {
            public string Answer { get; set; }
            public List<TavilyResult> Results { get; set; }
        }

        public class TavilyResult
        {
            public string Title { get; set; }
            public string Content { get; set; }
        }
    }
}
