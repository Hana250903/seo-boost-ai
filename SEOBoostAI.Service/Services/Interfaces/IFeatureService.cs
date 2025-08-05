using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IFeatureService
    {
        Task<List<Feature>> GetAllAsync();
        Task<Feature> GetByIdAsync(int id);
        Task<int> AddAsync(Feature feature);
        Task<int> UpdateAsync(Feature feature);
        Task<bool> DeleteAsync(int id);
    }
}
