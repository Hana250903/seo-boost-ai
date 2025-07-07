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
        Task<List<Keyword>> GetAllAsync();
        Task<PaginationResult<List<Keyword>>> GetAllAsync(KeywordSearchRequest keywordSearchRequest);
        Task<Keyword> GetByIdAsync(int id);
        Task<int> AddAsync(Keyword keyword);
        Task<int> UpdateAsync(Keyword keyword);
        Task<bool> DeleteAsync(int id);
    }
}
