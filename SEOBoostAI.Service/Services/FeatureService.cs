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
    public class FeatureService: IFeatureService
    {
        private readonly FeatureRepository _featureRepository;

        public FeatureService(FeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        public async Task<int> AddAsync(Feature feature)
        {
            return await _featureRepository.CreateAsync(feature);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var feature = await _featureRepository.GetByIdAsync(id);
            return await _featureRepository.RemoveAsync(feature);
        }

        public async Task<List<Feature>> GetAllAsync()
        {
            return await _featureRepository.GetAllAsync();
        }

        public async Task<Feature> GetByIdAsync(int id)
        {
            return await _featureRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(Feature feature)
        {
            return await _featureRepository.UpdateAsync(feature);
        }
    }
}
