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
    public class UserAccountSubscriptionRepository : GenericRepository<UserAccountSubscription>
    {
        public UserAccountSubscriptionRepository() => _context = new SEOBoostAIContext();
        public UserAccountSubscriptionRepository(SEOBoostAIContext context) => _context = context;

        public async Task<bool> IsExpired(int userId)
        {
            var subscription = await _context.UserAccountSubscriptions.FirstOrDefaultAsync(s => s.UserId == userId && s.IsActive == true);
            if (subscription == null)
            {
                return true; // No active subscription found
            }
            if (subscription.EndDate.HasValue && subscription.EndDate.Value < DateTime.UtcNow)
            {
                subscription.IsActive = false; // Mark as inactive if the subscription has expired
                _context.UserAccountSubscriptions.Update(subscription);
                await _context.SaveChangesAsync();
                return true; // Subscription has expired
            }
            return false; // Subscription is still active
        }
    }
}
