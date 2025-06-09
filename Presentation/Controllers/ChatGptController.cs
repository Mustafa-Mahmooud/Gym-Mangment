using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatGptController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ChatGptController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] ChatGptRequest request)
        {
            var apiKey = _configuration["ChatGpt:ApiKey"];
            var apiUrl = _configuration["ChatGpt:BaseUrl"];

            var payload = new
            {
                model = _configuration["ChatGpt:Model"],
                messages = new[]
                {
            new { role = "user", content = request.Prompt }
        }
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, apiUrl)
            {
                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            };
            httpRequest.Headers.Add("Authorization", $"Bearer {apiKey}");

            var response = await _httpClient.SendAsync(httpRequest);

            // Check for rate limit
            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                return StatusCode(429, "Rate limit exceeded. Please try again later.");
            }

           // response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(jsonResponse);
            var message = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message");

            string resultText = "";

            // Safe extraction of 'content' if it's a string
            if (message.TryGetProperty("content", out var contentProp) && contentProp.ValueKind == JsonValueKind.String)
            {
                resultText = contentProp.GetString();
            }
            else
            {
                resultText = "The response format was unexpected.";
            }

            return Ok(resultText);
        }
    }
        public class ChatGptRequest
    {
        public string Prompt { get; set; }
    }
}
