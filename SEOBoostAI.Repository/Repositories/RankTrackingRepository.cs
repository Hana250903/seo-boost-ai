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
    public class RankTrackingRepository: GenericRepository<RankTracking>
    {
        /// <summary>
/// Initializes a new instance of the <see cref="RankTrackingRepository"/> class with a default database context.
/// </summary>
public RankTrackingRepository() => _context ??= new SEOBoostAIContext();
        /// <summary>
/// Initializes a new instance of the <see cref="RankTrackingRepository"/> class with the specified database context.
/// </summary>
/// <param name="context">The database context to be used by the repository.</param>
public RankTrackingRepository(SEOBoostAIContext context) => _context = context;

        /// <summary>
        /// Asynchronously retrieves all rank tracking records for a specific user where the keyword contains the specified search term.
        /// </summary>
        /// <param name="keyword">The search term to match within the keyword field.</param>
        /// <param name="userId">The identifier of the user whose rank tracking records are to be retrieved.</param>
        /// <returns>A list of <see cref="RankTracking"/> entities matching the criteria, or an empty list if none are found.</returns>
        public async Task<List<RankTracking>> GetAllAsync(string keyword, int userId)
        {
            var termRankTracking = await _context.RankTrackings
                .Where(x => x.Keyword.Contains(keyword) && x.UserId == userId)
                .ToListAsync();
            return termRankTracking ?? new List<RankTracking>();
        }
    }
}
