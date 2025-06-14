using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services.Interfaces;

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditReportsController : ControllerBase
    {
        private readonly IAuditReportService _auditReportService;
        public AuditReportsController(IAuditReportService auditReportService)
        {
            _auditReportService = auditReportService;
        }

        [HttpGet]
        public async Task<List<AuditReport>> GetAllAudit()
        {
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

        /// <summary>
        /// Deletes the audit report with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the audit report to delete.</param>
        /// <returns>True if the audit report was successfully deleted; otherwise, false.</returns>
        [HttpDelete]
        public async Task<bool> DeleteAuditReport(int id)
        {
            return await _auditReportService.DeleteAuditAsync(id);
        }

        /// <summary>
        /// Analyzes the specified URL for a given user and returns the audit analysis result.
        /// </summary>
        /// <param name="userId">The ID of the user requesting the analysis.</param>
        /// <param name="url">The URL to be analyzed, provided as a query parameter.</param>
        /// <returns>
        /// An HTTP 200 response with the analysis result if successful; otherwise, an HTTP 400 response with an error message.
        /// </returns>
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
    }
}
