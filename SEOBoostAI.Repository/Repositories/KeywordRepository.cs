using Microsoft.EntityFrameworkCore;
using SEOBoostAI.Repository.GenericRepository;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Repository.Repositories
{
    public class KeywordRepository: GenericRepository<Keyword>
    {
        /// <summary>
/// Initializes a new instance of the <see cref="KeywordRepository"/> class with a default database context if none is set.
/// </summary>
public KeywordRepository() => _context ??= new SEOBoostAIContext();
        /// <summary>
/// Initializes a new instance of the <see cref="KeywordRepository"/> class with the specified database context.
/// </summary>
/// <param name="context">The database context to be used by the repository.</param>
public KeywordRepository(SEOBoostAIContext context) => _context = context;
        
        /// <summary>
        /// Asynchronously retrieves all keywords containing the specified substring.
        /// </summary>
        /// <param name="keyword">The substring to search for within keyword entries.</param>
        /// <returns>A list of <see cref="Keyword"/> objects whose Keyword1 property contains the specified substring. Returns an empty list if no matches are found.</returns>
        public async Task<List<Keyword>> GetAllAsync(string keyword)
        {
            var list = await _context.Keywords.Where(k => k.Keyword1.Contains(keyword)).ToListAsync();

            return list ?? new List<Keyword>();
        }
    }
}
