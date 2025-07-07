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
        /// Retrieves all keywords asynchronously.
        /// </summary>
        /// <returns>A list of all <see cref="Keyword"/> entities.</returns>
        public async Task<List<Keyword>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Retrieves a paginated list of keywords based on the specified search criteria.
        /// </summary>
        /// <param name="keywordSearchRequest">The search and pagination parameters for filtering keywords.</param>
        /// <returns>A pagination result containing a list of keywords matching the search criteria.</returns>
        public async Task<PaginationResult<List<Keyword>>> GetAllAsync(KeywordSearchRequest keywordSearchRequest)
        {
            return await _repository.GetAllAsync(keywordSearchRequest);
        }

        /// <summary>
        /// Retrieves a keyword entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the keyword to retrieve.</param>
        /// <returns>The keyword entity with the specified ID, or null if not found.</returns>
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
