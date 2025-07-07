using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEOBoostAI.API.VIewModels.Requests;
using SEOBoostAI.API.VIewModels.Responses;
using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services;
using SEOBoostAI.Service.Services.Interfaces;
using SEOBoostAI.Service.Ultils;
using System.Net.Http;

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditReportsController : ControllerBase
    {
        private readonly IAuditReportService _auditReportService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IElementService _elementService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly string _flaskApiBaseUrl = "http://127.0.0.1:5001";
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditReportsController"/> class with the specified services and HTTP client.
        /// </summary>
        /// <param name="auditReportService">Service for managing audit reports.</param>
        /// <param name="currentUserService">Service for accessing the current user context.</param>
        /// <param name="elementService">Service for managing audit elements.</param>
        /// <param name="mapper">Object mapper for model transformations.</param>
        /// <param name="httpClient">HTTP client for external API communication.</param>

        public AuditReportsController(IAuditReportService auditReportService, ICurrentUserService currentUserService, IElementService elementService, IMapper mapper,
            HttpClient httpClient)
        {
            _auditReportService = auditReportService;
            _currentUserService = currentUserService;
            _elementService = elementService;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Retrieves a list of all audit reports.
        /// </summary>
        /// <returns>A list of all <see cref="AuditReport"/> objects.</returns>
        [HttpGet]
        public async Task<List<AuditReport>> GetAllAudit()
        {
            //var userId = _currentUserService.GetUserId();
            return await _auditReportService.GetAllAuditAsync();
        }

        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<PaginationResult<List<AuditReport>>> GetAllPagination(int currentPage, int pageSize)
        {
            return await _auditReportService.GetAuditWithPaginateAsync(currentPage, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<AuditReport> GetAuditReportById(int id)
        {
            return await _auditReportService.GetAuditByIdAsync(id);
        }

        [HttpPost]
        public async Task<int> CreateAuditReport(AuditReport AuditReport)
        {
            return await _auditReportService.CreateAuditAsync(AuditReport);
        }

        [HttpPut]
        public async Task<int> UpdateAuditReport(AuditReport AuditReport)
        {
            return await _auditReportService.UpdateAuditAsync(AuditReport);
        }

        [HttpDelete]
        public async Task<bool> DeleteAuditReport(int id)
        {
            return await _auditReportService.DeleteAuditAsync(id);
        }

        [HttpGet("analyze-url/{userId}")]
        public async Task<IActionResult> AnalyzeUrl(int userId, [FromQuery] string url)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url)) return BadRequest("Missing URL");

                var decodedUrl = System.Net.WebUtility.UrlDecode(url); // Optional, thường không cần nếu ASP.NET tự decode
                var result = await _auditReportService.AuditAnalyze(decodedUrl, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves all audit reports associated with the specified user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose audit reports are to be retrieved.</param>
        /// <returns>A list of audit reports belonging to the specified user.</returns>
        [HttpGet("user/{userId}")]
        public async Task<List<AuditReport>> GetByUserId(int userId)
        {
            return await _auditReportService.GetByUserId(userId);
        }

        /// <summary>
        /// Prepares and returns an SEO advisory request object for a given audit report, including its failed elements.
        /// </summary>
        /// <param name="auditId">The ID of the audit report to analyze.</param>
        /// <returns>
        /// An HTTP 200 response containing the constructed advisory request object if failed elements are found; otherwise, a 400 Bad Request if no failed elements exist for the specified audit report.
        /// </returns>
        [HttpPost("advisor")]
        public async Task<IActionResult> SEOAdvisor([FromBody] int auditId)
        {
            var auditRequest = await _auditReportService.GetAuditByIdAsync(auditId);
            var element = await _elementService.ElementNotPass(auditId);

            if (element == null || !element.Any())
            {
                return BadRequest("No failed elements found for the provided audit report ID.");
            }

            var elementFailed = _mapper.Map<List<SEOBoostAI.API.VIewModels.Requests.Element>>(element);

            var auditReportFlashApi = new AuditRepostFlashApiRequest
            {
                Id = auditRequest.Id,
                UserId = auditRequest.UserId,
                Url = auditRequest.Url,
                OverallScore = auditRequest.OverallScore,
                CriticalIssue = auditRequest.CriticalIssue,
                Warning = auditRequest.Warning,
                Opportunity = auditRequest.Opportunity,
                PassedCheck = auditRequest.PassedCheck,
                FailedElements = elementFailed
            };

            //string flaskApiEndpoint = $"{_flaskApiBaseUrl}/seo-advisor";

            //try
            //{
            //    // Send a POST request to the Flask API
            //    // The 'PostAsJsonAsync' method automatically serializes the 'input' object to JSON
            //    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(flaskApiEndpoint, auditReportFlashApi);

            //    // Ensure the request was successful
            //    response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not 2xx

            //    // Read and deserialize the JSON response from the Flask API
            //    // The 'ReadFromJsonAsync' method automatically deserializes the JSON to FlaskApiResponse object
            //    AuditAdvisorResponse? flaskApiResponse = await response.Content.ReadFromJsonAsync<AuditAdvisorResponse>();

            //    if (flaskApiResponse == null)
            //    {
            //        return StatusCode(500, "Failed to deserialize response from Flask API.");
            //    }

            //    return Ok(flaskApiResponse); // Return the Flask API's response to the client
            //}
            //catch (HttpRequestException ex)
            //{
            //    // Handle HTTP request errors (e.g., network issues, Flask API not running)
            //    Console.Error.WriteLine($"HTTP Request Error: {ex.Message}");
            //    return StatusCode(500, $"Error communicating with Flask API: {ex.Message}");
            //}
            //catch (JsonException ex)
            //{
            //    // Handle JSON deserialization errors
            //    Console.Error.WriteLine($"JSON Deserialization Error: {ex.Message}");
            //    return StatusCode(500, $"Error processing Flask API response: {ex.Message}");
            //}
            //catch (Exception ex)
            //{
            //    // Handle any other unexpected errors
            //    Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
            //    return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            //}

            return Ok(auditReportFlashApi);
        }
    }
}
