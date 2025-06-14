using Microsoft.EntityFrameworkCore;
using SEOBoostAI.Repository.GenericRepository;
using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Repository.Repositories
{
    public class AuditReportRepository : GenericRepository<AuditReport>
    {
        public AuditReportRepository() { }

        public async Task<PaginationResult<List<AuditReport>>> GetAuditWithPaginateAsync(int currentPage, int pageSize)
        {
            var auditReports = await GetAllAsync();

            var totalItems = auditReports.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            auditReports = auditReports
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PaginationResult<List<AuditReport>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = auditReports
            };
            return result;
        }

        public async Task<int> CreateAndGetIdAsync(AuditReport auditReport)
        {
            _context.AuditReports.Add(auditReport);
            await _context.SaveChangesAsync();
            return auditReport.Id; // Lúc này auditReport.Id đã được gán tự động bởi DB
        }

        public async Task<List<AuditReport>> GetAllAsync(int userId)
        {
            var auditReports = await _context.AuditReports
                .Where(a => a.UserId == userId)
                .Include(a => a.Elements)
                .ToListAsync();

            return auditReports ?? new List<AuditReport>();
        }
    }
}
