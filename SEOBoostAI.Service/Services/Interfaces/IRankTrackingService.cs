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
        Task<PaginationResult<List<RankTracking>>> GetAllAsync(int currentPage, int pageSize);
        Task<RankTracking> GetByIdAsync(int id);
        Task<int> AddAsync(RankTracking rankTracking);
        Task<int> UpdateAsync(RankTracking rankTracking);
        Task<int> DeleteAsync(int id);
    }
}
