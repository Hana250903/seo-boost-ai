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
/// Asynchronously retrieves all <see cref="Keyword"/> entities.
/// </summary>
/// <returns>A task representing the asynchronous operation, containing a list of all keywords.</returns>
Task<List<Keyword>> GetAllAsync();
        /// <summary>
/// Asynchronously retrieves a list of keywords that match or are related to the specified keyword string.
/// </summary>
/// <param name="keyword">The keyword string to filter the results.</param>
/// <returns>A task representing the asynchronous operation, containing a list of matching <see cref="Keyword"/> objects.</returns>
Task<List<Keyword>> GetAllAsync(string keyword);
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
/// Updates an existing Keyword entity asynchronously.
/// </summary>
/// <param name="keyword">The Keyword object containing updated information.</param>
/// <returns>The number of records affected by the update operation.</returns>
Task<int> UpdateAsync(Keyword keyword);
        Task<bool> DeleteAsync(int id);
    }
}
