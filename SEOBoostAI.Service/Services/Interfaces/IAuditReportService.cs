using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IAuditReportService
    {
        Task<List<AuditReport>> GetAllAuditAsync();
        Task<PaginationResult<List<AuditReport>>> GetAuditWithPaginateAsync(int currentPage, int pageSize);
        Task<AuditReport> GetAuditByIdAsync(int id);
        Task<int> CreateAuditAsync(AuditReport auditReport);
        Task<int> UpdateAuditAsync(AuditReport auditReport);
        Task<bool> DeleteAuditAsync(int id);
        Task<AuditReport> AuditAnalyze(string url, int userId);
    }
}
