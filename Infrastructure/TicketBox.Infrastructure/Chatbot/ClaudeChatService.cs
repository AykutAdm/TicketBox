using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Interfaces.Services;

namespace TicketBox.Infrastructure.Chatbot
{
    public class ClaudeChatService : IClaudeChatService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ClaudeChatService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> AskAsync(string systemPrompt, string userMessage)
        {
            var apiKey = _configuration["ClaudeSettings:ApiKey"];
            var model = _configuration["ClaudeSettings:Model"];

            var requestBody = new
            {
                model = model,
                max_tokens = 500,
                system = systemPrompt,
                messages = new[]
                {
                    new { role = "user", content = userMessage }
                }
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

            var response = await client.PostAsync("https://api.anthropic.com/v1/messages", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Claude API hatası: {responseJson}");

            var result = JsonConvert.DeserializeObject<ClaudeResponse>(responseJson);
            var textBlock = result.Content.FirstOrDefault(c => c.Type == "text");
            return textBlock.Text;
        }

        public class ClaudeResponse
        {
            public List<ClaudeContentBlock> Content { get; set; }
        }

        public class ClaudeContentBlock
        {
            public string Type { get; set; }
            public string Text { get; set; }
        }
    }
}
