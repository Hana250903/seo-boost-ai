using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IContentOptimizationService
    {
        Task<List<ContentOptimization>> GetAllAsync();
        Task<ContentOptimization> GetByIdAsync(int id);
        Task<int> AddAsync(ContentOptimization contentOptimization);
        Task<int> UpdateAsync(ContentOptimization contentOptimization);
        Task<bool> DeleteAsync(int id);
    }
}
