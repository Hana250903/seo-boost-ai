using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IRankTrackingService
    {
        /// <summary>
/// Asynchronously retrieves all RankTracking records.
/// </summary>
/// <returns>A task representing the asynchronous operation, containing a list of all RankTracking entities.</returns>
Task<List<RankTracking>> GetAllAsync();
        /// <summary>
/// Retrieves a list of RankTracking records filtered by the specified keyword and user ID.
/// </summary>
/// <param name="keyword">The keyword to filter RankTracking records.</param>
/// <param name="userId">The user ID to filter RankTracking records.</param>
/// <returns>A task representing the asynchronous operation, containing a list of matching RankTracking records.</returns>
Task<List<RankTracking>> GetAllAsync(string keyword, int userId);
        /// <summary>
/// Retrieves a RankTracking record by its unique identifier asynchronously.
/// </summary>
/// <param name="id">The unique identifier of the RankTracking record.</param>
/// <returns>A task representing the asynchronous operation, containing the RankTracking record if found; otherwise, null.</returns>
Task<RankTracking> GetByIdAsync(int id);
        /// <summary>
/// Asynchronously adds a new RankTracking record.
/// </summary>
/// <param name="rankTracking">The RankTracking entity to add.</param>
/// <returns>The unique identifier of the newly added RankTracking record.</returns>
Task<int> AddAsync(RankTracking rankTracking);
        /// <summary>
/// Updates an existing RankTracking record asynchronously.
/// </summary>
/// <param name="rankTracking">The RankTracking entity with updated values.</param>
/// <returns>The number of records affected.</returns>
Task<int> UpdateAsync(RankTracking rankTracking);
        Task<bool> DeleteAsync(int id);
    }
}
