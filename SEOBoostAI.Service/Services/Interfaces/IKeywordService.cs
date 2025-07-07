using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IKeywordService
    {
        /// <summary>
/// Asynchronously retrieves all keywords.
/// </summary>
/// <returns>A task representing the asynchronous operation, containing a list of all <see cref="Keyword"/> objects.</returns>
Task<List<Keyword>> GetAllAsync();
        /// <summary>
/// Retrieves a paginated list of keywords based on the specified search criteria.
/// </summary>
/// <param name="keywordSearchRequest">The search and pagination parameters for filtering keywords.</param>
/// <returns>A paginated result containing a list of keywords matching the search criteria.</returns>
Task<PaginationResult<List<Keyword>>> GetAllAsync(KeywordSearchRequest keywordSearchRequest);
        /// <summary>
/// Asynchronously retrieves a <see cref="Keyword"/> by its unique identifier.
/// </summary>
/// <param name="id">The unique identifier of the keyword to retrieve.</param>
/// <returns>A task representing the asynchronous operation, containing the matching <see cref="Keyword"/> if found; otherwise, null.</returns>
Task<Keyword> GetByIdAsync(int id);
        /// <summary>
/// Asynchronously adds a new Keyword entity.
/// </summary>
/// <param name="keyword">The Keyword object to add.</param>
/// <returns>The identifier of the newly added Keyword.</returns>
Task<int> AddAsync(Keyword keyword);
        /// <summary>
/// Updates an existing Keyword entity.
/// </summary>
/// <param name="keyword">The Keyword object containing updated information.</param>
/// <returns>The number of records affected by the update operation.</returns>
Task<int> UpdateAsync(Keyword keyword);
        Task<bool> DeleteAsync(int id);
    }
}
