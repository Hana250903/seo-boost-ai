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
        Task<PaginationResult<List<Keyword>>> GetAllAsync(string keyword, int pageIndex, int pageSize);
        Task<Keyword> GetByIdAsync(int id);
        Task<int> AddAsync(Keyword keyword);
        Task<int> UpdateAsync(Keyword keyword);
        Task<bool> DeleteAsync(int id);
    }
}
