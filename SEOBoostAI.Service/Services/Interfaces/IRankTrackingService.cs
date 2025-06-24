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
        Task<List<RankTracking>> GetAllAsync();
        Task<PaginationResult<List<RankTracking>>> GetAllAsync(int userId, int pageIndex, int pageSize);
        Task<RankTracking> GetByIdAsync(int id);
        Task<int> AddAsync(RankTracking rankTracking);
        Task<int> UpdateAsync(RankTracking rankTracking);
        Task<int> UpdateAsync(List<RankTrackingRequest> rankTrackings);
        Task<bool> DeleteAsync(int id);
    }
}
