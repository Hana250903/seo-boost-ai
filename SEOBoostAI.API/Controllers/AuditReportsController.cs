using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services.Interfaces;
using SEOBoostAI.Service.Ultils;

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditReportsController : ControllerBase
    {
        private readonly IAuditReportService _auditReportService;
        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditReportsController"/> with the specified audit report and current user services.
        /// </summary>
        public AuditReportsController(IAuditReportService auditReportService, ICurrentUserService currentUserService)
        {
            _auditReportService = auditReportService;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Retrieves a list of all audit reports.
        /// </summary>
        /// <returns>A list of <see cref="AuditReport"/> objects.</returns>
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
    }
}
