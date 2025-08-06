using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Repository.Repositories;
using SEOBoostAI.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services
{
    public class RankTrackingService : IRankTrackingService
    {
        private readonly RankTrackingRepository _repository;

        public RankTrackingService() => _repository ??= new RankTrackingRepository();

        public async Task<int> AddAsync(RankTracking rankTracking)
        {
            return await _repository.CreateAsync(rankTracking);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rankTracking = await _repository.GetByIdAsync(id);
            return await _repository.RemoveAsync(rankTracking);
        }

        public async Task<List<RankTracking>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PaginationResult<List<RankTracking>>> GetAllAsync(int userId, int pageIndex, int pageSize)
        {
            return await _repository.GetAllAsync(userId, pageIndex, pageSize);
        }

        public async Task<RankTracking> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<RankTracking>> GetByUserIdAsync(int userId)
        {
            return await _repository.GetRankTrackingsByUserId(userId);
        }

        public async Task<int> UpdateAsync(RankTracking rankTracking)
        {
            return await _repository.UpdateAsync(rankTracking);
        }

        public async Task<int> UpdateAsync(List<RankTrackingRequest> rankTrackings)
        {
            return await _repository.UpdateAsync(rankTrackings);
        }
    }
}
