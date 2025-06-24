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
    public class ContentOptimizationRepository : GenericRepository<ContentOptimization>
    {
        public ContentOptimizationRepository() => _context ??= new SEOBoostAIContext();

        public ContentOptimizationRepository(SEOBoostAIContext context) => _context = context;

        //New ContinueWith : ưu vẫn trả entity vừa tạo, nhược điểm là không await được, nên không thể sử dụng trong các phương thức async khác.
        //public async Task<ContentOptimization> CreateAsync(ContentOptimization contentOptimization)
        //{
        //    _context.ContentOptimizations.Add(contentOptimization);
        //    return await _context.SaveChangesAsync().ContinueWith(t => contentOptimization);
        //}

        public async Task<ContentOptimization> CreateEntityAsync(ContentOptimization contentOptimization)
        {
            _context.ContentOptimizations.Add(contentOptimization);
            await _context.SaveChangesAsync();
            return contentOptimization;
        }

        public async Task<List<ContentOptimization>> GetByUserIdAsync(int userId)
        {
            return await _context.ContentOptimizations.Where(co => co.UserId == userId).ToListAsync();
        }

    }
}
