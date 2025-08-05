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
    public class UserAccountSubscriptionService : IUserAccountSubscriptionService
    {
        private readonly UserAccountSubscriptionRepository _userAccountSubscription;

        public UserAccountSubscriptionService(UserAccountSubscriptionRepository userAccountSubscription)
        {
            _userAccountSubscription = userAccountSubscription;
        }

        public async Task<int> AddAsync(UserAccountSubscription userAccountSubscription)
        {
            return await _userAccountSubscription.CreateAsync(userAccountSubscription);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userAccountSubscription = await _userAccountSubscription.GetByIdAsync(id);
            return await _userAccountSubscription.RemoveAsync(userAccountSubscription);
        }

        public async Task<List<UserAccountSubscription>> GetAllAsync()
        {
            return await _userAccountSubscription.GetAllAsync();
        }

        public async Task<UserAccountSubscription> GetByIdAsync(int id)
        {
            return await _userAccountSubscription.GetByIdAsync(id);
        }

        public async Task<bool> IsExpired(int userId)
        {
            return await _userAccountSubscription.IsExpired(userId);
        }

        public async Task<int> UpdateAsync(UserAccountSubscription userAccountSubscription)
        {
            return await _userAccountSubscription.UpdateAsync(userAccountSubscription);
        }
    }
}
