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

        public async Task<PaginationResult<List<RankTracking>>> GetAllAsync(int userId, int pageIndex, int pageSize)
        {
            var termRankTracking = await _context.RankTrackings
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var totalItems = termRankTracking.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            termRankTracking = termRankTracking
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PaginationResult<List<RankTracking>>
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = termRankTracking
            };

            return result ?? new PaginationResult<List<RankTracking>>();
        }

        public async Task<int> UpdateAsync(List<RankTrackingRequest> rankTrackingRequests)
        {
            var ids = rankTrackingRequests.Select(r => r.Id).ToList();

            var rankTrackings = await _context.RankTrackings
                .Where(rt => ids.Contains(rt.Id))
                .ToListAsync();

            foreach (var tracking in rankTrackings)
            {
                var request = rankTrackingRequests.FirstOrDefault(r => r.Id == tracking.Id);
                if (request != null)
                {
                    tracking.Rank = request.Rank;
                }
            }
            _context.RankTrackings.UpdateRange(rankTrackings);
            return await _context.SaveChangesAsync();
        }

    }
}
