using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEOBoostAI.API.VIewModels.Requests;
using SEOBoostAI.API.VIewModels.Responses;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services.Interfaces;
using SEOBoostAI.Service.Ultils;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankTrackingsController : ControllerBase
    {
        private readonly IRankTrackingService _service;
        private readonly ICurrentUserService _currentUserService;
        private readonly HttpClient _httpClient;
        private readonly string _flaskApiBaseUrl = "http://127.0.0.1:5001";

        public RankTrackingsController(IRankTrackingService service, ICurrentUserService currentUserService, HttpClient httpClient)
        {
            _service = service;
            _currentUserService = currentUserService;
            _httpClient = httpClient;
        }

        // GET: api/<RankTrackingsController>
        [HttpGet]
        public async Task<List<RankTracking>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<RankTrackingsController>/5
        [HttpGet("{id}")]
        public async Task<RankTracking> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        // POST api/<RankTrackingsController>
        [HttpPost]
        public async Task<int> Post(RankTracking rankTracking)
        {
            return await _service.AddAsync(rankTracking);
        }

        // PUT api/<RankTrackingsController>/5
        [HttpPut]
        public async Task<int> Put(RankTracking rankTracking)
        {
            return await _service.UpdateAsync(rankTracking);
        }

        // DELETE api/<RankTrackingsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("search/{userId}/{keyword}")]
        public async Task<List<RankTracking>> Get(string keyword, int userId)
        {
            return await _service.GetAllAsync(keyword, userId);
        }

        [HttpPost]
        [Route("rank-tracking")]
        public async Task<IActionResult> Get([FromBody] RankTrackingFlaskApiRequest inputKeyword)
        {

            if (string.IsNullOrEmpty(inputKeyword.InputKeyword))
            {
                return BadRequest("Input keyword cannot be empty.");
            }

            if (string.IsNullOrEmpty(inputKeyword.UserId))
            {
                return BadRequest("Input keyword cannot be empty.");
            }

            string flaskApiEndpoint = $"{_flaskApiBaseUrl}/rank-tracking";

            try
            {
                // Send a POST request to the Flask API
                // The 'PostAsJsonAsync' method automatically serializes the 'input' object to JSON
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(flaskApiEndpoint, inputKeyword);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not 2xx

                // Read and deserialize the JSON response from the Flask API
                // The 'ReadFromJsonAsync' method automatically deserializes the JSON to FlaskApiResponse object
                FlaskApiResponse? flaskApiResponse = await response.Content.ReadFromJsonAsync<FlaskApiResponse>();

                if (flaskApiResponse == null)
                {
                    return StatusCode(500, "Failed to deserialize response from Flask API.");
                }

                // You can now process the response from the Flask API as needed in your C# backend
                // For example, log the results, save them to a database, or return them to the frontend
                Console.WriteLine($"Flask API Message: {flaskApiResponse.Message}");
                Console.WriteLine($"Generated Keywords Count: {flaskApiResponse.GeneratedKeywordsCount}");
                foreach (var status in flaskApiResponse.ExternalApiStatus)
                {
                    Console.WriteLine($"  Keyword: {status.Keyword}, Status: {status.Status}, Message: {status.Message}");
                }

                return Ok(flaskApiResponse); // Return the Flask API's response to the client
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors (e.g., network issues, Flask API not running)
                Console.Error.WriteLine($"HTTP Request Error: {ex.Message}");
                return StatusCode(500, $"Error communicating with Flask API: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization errors
                Console.Error.WriteLine($"JSON Deserialization Error: {ex.Message}");
                return StatusCode(500, $"Error processing Flask API response: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
