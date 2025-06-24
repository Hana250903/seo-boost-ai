using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEOBoostAI.API.VIewModels.Requests;
using SEOBoostAI.API.VIewModels.Responses;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services;
using SEOBoostAI.Service.Services.Interfaces;
using SEOBoostAI.Service.Ultils;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentOptimizationsController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IContentOptimizationService _service;
        private readonly HttpClient _httpClient;
        //private readonly string _flaskApiBaseUrl = "http://127.0.0.1:5001";
        private readonly string _flaskApiBaseUrl = "https://seo-flask-api.azurewebsites.net";

        public ContentOptimizationsController(IContentOptimizationService service, ICurrentUserService currentUserService, HttpClient httpClient)
        {
            _service = service;
            _currentUserService = currentUserService;
            _httpClient = httpClient;
        }

        // GET: api/<ContentOptimizationsController>
        [HttpGet]
        public async Task<List<ContentOptimization>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<ContentOptimizationsController>/5
        [HttpGet("user/{userId}")]
        public async Task<List<ContentOptimization>> GetByUserId(int userId)
        {
            return await _service.GetByUserIdAsync(userId);
        }

        [HttpGet("{id}")]
        public async Task<ContentOptimization> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        // POST api/<ContentOptimizationsController>
        [HttpPost]
        public async Task<ContentOptimization> Post(ContentOptimization contentOptimization)
        {
            var content = await _service.AddAsync(contentOptimization);

            if (content == null)
            {
                throw new Exception("Content optimization failed to be created.");
            }

            // Call the Flask API to optimize content
            string flaskApiEndpoint = $"{_flaskApiBaseUrl}/optimize-content";

            var requestData = new ContentFlaskApiRequest
            {
                Id = content.Id,
                UserId = content.UserId,
                Keyword = content.Keyword,
                Content = content.OriginalContent,
                ContentLenght = content.ContentLenght,
                OptimizationLevel = content.OptimizationLevel,
                ReadabilityLevel = content.ReadabilityLevel,
                IncludeCitation = content.IncludeCitation.Value,
            };

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(flaskApiEndpoint, requestData);

                response.EnsureSuccessStatusCode();

                FlaskApiResponse? flaskApiResponse = await response.Content.ReadFromJsonAsync<FlaskApiResponse>();

                content = await _service.GetByIdAsync(content.Id);

                if (flaskApiResponse == null || content == null)
                {
                    throw new Exception("Code 500, failed to deserialize response from Flask API.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors (e.g., network issues, Flask API not running)
                Console.Error.WriteLine($"HTTP Request Error: {ex.Message}");
                throw new Exception($"Error communicating with Flask API: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization errors
                Console.Error.WriteLine($"JSON Deserialization Error: {ex.Message}");
                throw new Exception($"Error processing Flask API response: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error communicating with Flask API: {ex.Message}");
            }

            return content;
        }

        // PUT api/<ContentOptimizationsController>/5
        [HttpPut]
        public async Task<int> Put(ContentOptimization contentOptimization)
        {
            return await _service.UpdateAsync(contentOptimization);
        }

        // DELETE api/<ContentOptimizationsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
