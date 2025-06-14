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

        /// <summary>
        /// Asynchronously retrieves all RankTracking entities.
        /// </summary>
        /// <returns>A list of all RankTracking entities.</returns>
        public async Task<List<RankTracking>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Asynchronously retrieves all RankTracking entities that match the specified keyword and belong to the given user.
        /// </summary>
        /// <param name="keyword">The keyword to filter RankTracking entities.</param>
        /// <param name="userId">The user ID to filter RankTracking entities.</param>
        /// <returns>A list of RankTracking entities matching the criteria.</returns>
        public async Task<List<RankTracking>> GetAllAsync(string keyword, int userId)
        {
            return await _repository.GetAllAsync(keyword, userId);
        }

        /// <summary>
        /// Asynchronously retrieves a RankTracking entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the RankTracking entity.</param>
        /// <returns>The RankTracking entity with the specified ID, or null if not found.</returns>
        public async Task<RankTracking> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(RankTracking rankTracking)
        {
            return await _repository.UpdateAsync(rankTracking);
        }
    }
}
