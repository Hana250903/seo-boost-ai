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
    public class KeywordService : IKeywordService
    {
        private readonly KeywordRepository _repository;

        public KeywordService() => _repository = new KeywordRepository();

        public async Task<int> AddAsync(Keyword keyword)
        {
            return await _repository.CreateAsync(keyword);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var keyword = await _repository.GetByIdAsync(id);
            return await _repository.RemoveAsync(keyword);
        }

        /// <summary>
        /// Asynchronously retrieves all Keyword entities.
        /// </summary>
        /// <returns>A list of all Keyword objects.</returns>
        public async Task<List<Keyword>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Asynchronously retrieves all Keyword entities that match the specified keyword string.
        /// </summary>
        /// <param name="keyword">The keyword string to filter the Keyword entities.</param>
        /// <returns>A list of Keyword entities matching the provided keyword.</returns>
        public async Task<List<Keyword>> GetAllAsync(string keyword)
        {
            return await _repository.GetAllAsync(keyword);
        }

        /// <summary>
        /// Asynchronously retrieves a Keyword entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Keyword to retrieve.</param>
        /// <returns>The Keyword entity with the specified ID, or null if not found.</returns>
        public async Task<Keyword> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(Keyword keyword)
        {
            return await _repository.UpdateAsync(keyword);
        }
    }
}
