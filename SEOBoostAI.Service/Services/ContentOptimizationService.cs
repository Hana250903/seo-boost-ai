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
    public class ContentOptimizationService : IContentOptimizationService
    {
        private readonly ContentOptimizationRepository _repository;

        public ContentOptimizationService() => _repository ??= new ContentOptimizationRepository();


        public async Task<int> AddAsync(ContentOptimization contentOptimization)
        {
            return await _repository.CreateAsync(contentOptimization);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contentOptimization = await _repository.GetByIdAsync(id);
            return await _repository.RemoveAsync(contentOptimization);
        }

        public async Task<List<ContentOptimization>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ContentOptimization> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(ContentOptimization contentOptimization)
        {
            return await _repository.UpdateAsync(contentOptimization);
        }
    }
}
