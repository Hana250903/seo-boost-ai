using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Repository.Repositories;
using SEOBoostAI.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services
{
    public class AuditReportService : IAuditReportService
    {
        private readonly AuditReportRepository _auditReportRepository;
        private readonly ElementService _elementService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditReportService"/> class with default repository and element service implementations.
        /// </summary>
        public AuditReportService()
        {
            _auditReportRepository ??= new AuditReportRepository();
            _elementService ??= new ElementService();
        }

        /// <summary>
        /// Performs a full SEO audit analysis for the specified URL and user, creating an audit report, analyzing page elements, calculating audit metrics, and updating the report with results.
        /// </summary>
        /// <param name="url">The URL of the page to audit.</param>
        /// <param name="userId">The ID of the user initiating the audit.</param>
        /// <returns>The completed and updated <see cref="AuditReport"/> containing analysis results.</returns>
        /// <exception cref="Exception">
        /// Thrown if the audit report cannot be created, if no elements are found for the URL, if element creation fails, or if updating the audit report fails.
        /// </exception>
        public async Task<AuditReport> AuditAnalyze(string url, int userId)
        {
            var result = 0;

            var auditReport = new AuditReport
            {
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                Url = url,
                CriticalIssue = 0,
                Warning = 0,
                Opportunity = 0,
                OverallScore = 0,
                PassedCheck = ""
            };

            var auditId = await _auditReportRepository.CreateAndGetIdAsync(auditReport);
            if (auditId <= 0)
            {
                throw new Exception("Failed to create audit report.");
            }

            List<Element> elements = await _elementService.AnalyzePageAsync(url, auditId);

            if (elements == null || !elements.Any())
            {
                throw new Exception("No elements found for the provided URL.");
            }

            result = await _elementService.CreateElementsAsync(elements);
            if (result <= 0)
            {
                throw new Exception("Failed to create elements for the audit report.");
            }

            // Cập nhật thông tin tính toán tổng quan
            auditReport.Id = auditId;
            auditReport.CriticalIssue = elements.Count(e => e.Important == 2 && e.Status == "not pass");
            auditReport.Warning = auditReport.CriticalIssue;
            auditReport.Opportunity = elements.Count(e => e.Important == 1 && e.Status == "not pass");
            int passed = elements.Count(e => e.Status == "pass");
            int total = elements.Count;
            auditReport.OverallScore = total == 0 ? 0 : (int)Math.Round((double)passed / total * 100);
            auditReport.PassedCheck = $"{passed}/{total} element passed";

            result = await _auditReportRepository.UpdateAsync(auditReport);

            if (result <= 0)
            {
                throw new Exception("Failed to update audit report with calculated scores.");
            }

            return auditReport;
        }

        /// <summary>
        /// Persists a new audit report and returns its unique identifier.
        /// </summary>
        /// <param name="auditReport">The audit report to be created.</param>
        /// <returns>The unique identifier of the created audit report.</returns>
        public async Task<int> CreateAuditAsync(AuditReport auditReport)
        {
            return await _auditReportRepository.CreateAsync(auditReport);
        }

        public async Task<bool> DeleteAuditAsync(int id)
        {
            var audit = await _auditReportRepository.GetByIdAsync(id);
            return await _auditReportRepository.RemoveAsync(audit);
        }

        public async Task<List<AuditReport>> GetAllAuditAsync()
        {
            return await _auditReportRepository.GetAllAsync();
        }

        public async Task<AuditReport> GetAuditByIdAsync(int id)
        {
            return await _auditReportRepository.GetByIdAsync(id);
        }

        public async Task<PaginationResult<List<AuditReport>>> GetAuditWithPaginateAsync(int currentPage, int pageSize)
        {
            return await _auditReportRepository.GetAuditWithPaginateAsync(currentPage, pageSize);
        }

        public async Task<int> UpdateAuditAsync(AuditReport auditReport)
        {
            return await _auditReportRepository.UpdateAsync(auditReport);
        }
    }
}
