using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using SEOBoostAI.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services
{
    public class RankTrackingService : IRankTrackingService
    {
        //private readonly UnitOfWork<RankTracking> _unitOfWork;

        //public RankTrackingService(UnitOfWork<RankTracking> unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        public Task<int> AddAsync(RankTracking rankTracking)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationResult<List<RankTracking>>> GetAllAsync(int currentPage, int pageSize)
        {
            //return await _unitOfWork.Repository.GetAllAsync(currentPage, pageSize);
            throw new NotImplementedException();
        }

        public Task<RankTracking> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(RankTracking rankTracking)
        {
            throw new NotImplementedException();
        }
    }
}
