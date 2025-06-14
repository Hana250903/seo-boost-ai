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
        public RankTrackingRepository() => _context ??= new SEOBoostAIContext();
        public RankTrackingRepository(SEOBoostAIContext context) => _context = context;

        public async Task<List<RankTracking>> GetAllAsync(string keyword, int userId)
        {
            var termRankTracking = await _context.RankTrackings
                .Where(x => x.Keyword.Contains(keyword) && x.UserId == userId)
                .ToListAsync();
            return termRankTracking ?? new List<RankTracking>();
        }
    }
}
