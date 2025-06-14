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

        /// <summary>
        /// Retrieves a paginated list of all audit reports along with pagination metadata.
        /// </summary>
        /// <param name="currentPage">The current page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A <see cref="PaginationResult{List&lt;AuditReport&gt;}"/> containing the paginated audit reports and pagination details.</returns>
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

        /// <summary>
        /// Asynchronously creates a new audit report and returns its generated database ID.
        /// </summary>
        /// <param name="auditReport">The audit report entity to add.</param>
        /// <returns>The ID of the newly created audit report.</returns>
        public async Task<int> CreateAndGetIdAsync(AuditReport auditReport)
        {
            _context.AuditReports.Add(auditReport);
            await _context.SaveChangesAsync();
            return auditReport.Id; // Lúc này auditReport.Id đã được gán tự động bởi DB
        }

        /// <summary>
        /// Asynchronously retrieves all audit reports for a specified user, including their related elements.
        /// </summary>
        /// <param name="userId">The identifier of the user whose audit reports are to be fetched.</param>
        /// <returns>A list of <see cref="AuditReport"/> entities associated with the user, or an empty list if none are found.</returns>
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
