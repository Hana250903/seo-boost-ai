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
        public AuditReportService() => _auditReportRepository ??= new AuditReportRepository();

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
