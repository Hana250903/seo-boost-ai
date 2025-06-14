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
        /// <summary>
/// Asynchronously creates a new audit report.
/// </summary>
/// <param name="auditReport">The audit report to create.</param>
/// <returns>The ID of the newly created audit report.</returns>
Task<int> CreateAuditAsync(AuditReport auditReport);
        /// <summary>
/// Updates an existing audit report.
/// </summary>
/// <param name="auditReport">The audit report with updated information.</param>
/// <returns>The number of records affected by the update operation.</returns>
Task<int> UpdateAuditAsync(AuditReport auditReport);
        /// <summary>
/// Asynchronously deletes an audit report by its identifier.
/// </summary>
/// <param name="id">The unique identifier of the audit report to delete.</param>
/// <returns>True if the audit report was successfully deleted; otherwise, false.</returns>
Task<bool> DeleteAuditAsync(int id);
        /// <summary>
/// Performs an audit analysis on the specified URL for the given user.
/// </summary>
/// <param name="url">The URL to be analyzed.</param>
/// <param name="userId">The identifier of the user requesting the audit.</param>
/// <returns>An <see cref="AuditReport"/> containing the results of the analysis.</returns>
Task<AuditReport> AuditAnalyze(string url, int userId);
    }
}
