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
        Task<List<RankTracking>> GetAllAsync(string keyword, int userId);
        Task<RankTracking> GetByIdAsync(int id);
        Task<int> AddAsync(RankTracking rankTracking);
        Task<int> UpdateAsync(RankTracking rankTracking);
        Task<bool> DeleteAsync(int id);
    }
}
