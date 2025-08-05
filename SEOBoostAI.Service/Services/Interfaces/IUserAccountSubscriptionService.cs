using SEOBoostAI.Repository.ModelExtensions;
using SEOBoostAI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IUserAccountSubscriptionService
    {
        Task<List<UserAccountSubscription>> GetAllAsync();
        Task<UserAccountSubscription> GetByIdAsync(int id);
        Task<int> AddAsync(UserAccountSubscription userAccountSubscription);
        Task<int> UpdateAsync(UserAccountSubscription userAccountSubscription);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsExpired(int userId);
    }
}
