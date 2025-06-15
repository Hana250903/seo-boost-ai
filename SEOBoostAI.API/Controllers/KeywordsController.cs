using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SEOBoostAI.API.VIewModels.Requests;
using SEOBoostAI.API.VIewModels.Responses;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services.Interfaces;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordsController : ControllerBase
    {
        private readonly IKeywordService _service;
        private readonly HttpClient _httpClient;
        private readonly string _flaskApiBaseUrl = "http://127.0.0.1:5000";

        public KeywordsController(IKeywordService service, HttpClient httpClient)
        {
            _service = service;
            _httpClient = httpClient;
        }

        // GET: api/<KeywordsController>
        [HttpGet]
        public async Task<List<Keyword>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<KeywordsController>/5
        [HttpGet("{id}")]
        public async Task<Keyword> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        // POST api/<KeywordsController>
        [HttpPost]
        public async Task<int> Post(Keyword keyword)
        {
            return await _service.AddAsync(keyword);
        }

        // PUT api/<KeywordsController>/5
        [HttpPut]
        public async Task<int> Put(Keyword keyword)
        {
            return await _service.UpdateAsync(keyword);
        }

        // DELETE api/<KeywordsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }

        [HttpGet("search/{keyword}")]
        public async Task<List<Keyword>> Search(string keyword)
        {
            return await _service.GetAllAsync(keyword);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Get([FromBody]KeywordFlaskApiRequest inputKeyword)
        {

            if (string.IsNullOrEmpty(inputKeyword.InputKeyword))
            {
                return BadRequest("Input keyword cannot be empty.");
            }

            string flaskApiEndpoint = $"{_flaskApiBaseUrl}/generate-seo-keywords";

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
