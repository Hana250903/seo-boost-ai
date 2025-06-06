﻿using Microsoft.AspNetCore.Http;
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

        [HttpDelete]
        public async Task<bool> DeleteAuditReport(int id)
        {
            return await _auditReportService.DeleteAuditAsync(id);
        }
    }
}
